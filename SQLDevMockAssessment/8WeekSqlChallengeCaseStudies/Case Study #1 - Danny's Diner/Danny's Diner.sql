Use SqlCaseStudy
--------------------------------
--CASE STUDY #1: DANNY'S DINER--
--------------------------------



--Author: Michael Shepherd Munemo
--Date: 18/03/2023 (updated )
--Tool used: MS SQL Server

/*
CREATE SCHEMA dannys_diner;

CREATE TABLE dannys_diner.sales (
  "customer_id" VARCHAR(1),
  "order_date" DATE,
  "product_id" INTEGER
);

INSERT INTO dannys_diner.sales
  ("customer_id", "order_date", "product_id")
VALUES
  ('A', '2021-01-01', '1'),
  ('A', '2021-01-01', '2'),
  ('A', '2021-01-07', '2'),
  ('A', '2021-01-10', '3'),
  ('A', '2021-01-11', '3'),
  ('A', '2021-01-11', '3'),
  ('B', '2021-01-01', '2'),
  ('B', '2021-01-02', '2'),
  ('B', '2021-01-04', '1'),
  ('B', '2021-01-11', '1'),
  ('B', '2021-01-16', '3'),
  ('B', '2021-02-01', '3'),
  ('C', '2021-01-01', '3'),
  ('C', '2021-01-01', '3'),
  ('C', '2021-01-07', '3');
 

CREATE TABLE dannys_diner.menu (
  "product_id" INTEGER,
  "product_name" VARCHAR(5),
  "price" INTEGER
);

INSERT INTO dannys_diner.menu
  ("product_id", "product_name", "price")
VALUES ('1', 'sushi', '10'),('2', 'curry', '15'),('3', 'ramen', '12');
  

CREATE TABLE dannys_diner.members (
  "customer_id" VARCHAR(1),
  "join_date" DATE
);

INSERT INTO dannys_diner.members
  ("customer_id", "join_date")
VALUES ('A', '2021-01-07'),('B', '2021-01-09'); */

SELECT * FROM dannys_diner.members

SELECT * FROM dannys_diner.menu

SELECT * FROM dannys_diner.sales

------------------------
--CASE STUDY QUESTIONS--
------------------------

--1. What is the total amount each customer spent at the restaurant?

SELECT customer_id, SUM(price) AS total_amount
FROM dannys_diner.sales s
JOIN dannys_diner.menu men
	ON s.product_id = men.product_id
GROUP BY customer_id;

--2. How many days has each customer visited the restaurant?
SELECT customer_id, COUNT(DISTINCT(order_date)) AS count_visit
FROM dannys_diner.sales
GROUP BY customer_id;

--3. What was the first item from the menu purchased by each customer?
WITH ordered_sales_cte AS
(
	SELECT 
    customer_id, 
    order_date, 
    product_name,
		DENSE_RANK() OVER(PARTITION BY s.customer_id ORDER BY s.order_date) AS rank
	FROM dannys_diner.sales AS s
	JOIN dannys_diner.menu AS m
		ON s.product_id = m.product_id
)

SELECT 
  customer_id, 
  product_name
FROM ordered_sales_cte
WHERE rank = 1
GROUP BY customer_id, product_name;

--4. What is the most purchased item on the menu and how many times was it purchased by all customers?
SELECT * FROM dannys_diner.menu; select * from dannys_diner.sales

SELECT COUNT(s.product_id)AS most_purchased, m.product_name
FROM dannys_diner.sales s
JOIN dannys_diner.menu m
	ON s.product_id = m.product_id
GROUP BY s.product_id, m.product_name
ORDER BY most_purchased DESC;


--5. Which item was the most popular for each customer?
WITH fav_item_cte AS
(
	SELECT 
    s.customer_id, 
    m.product_name, 
    COUNT(m.product_id) AS order_count,
		DENSE_RANK() OVER(PARTITION BY s.customer_id ORDER BY COUNT(s.customer_id) DESC) AS rank
FROM dannys_diner.menu AS m
JOIN dannys_diner.sales AS s
	ON m.product_id = s.product_id
GROUP BY s.customer_id, m.product_name
)

SELECT 
  customer_id, 
  product_name, 
  order_count
FROM fav_item_cte 
WHERE rank = 1;

--6. Which item was purchased first by the customer after they became a member?
WITH member_sales_cte AS 
(
  SELECT 
    s.customer_id, 
    m.join_date, 
    s.order_date, 
    s.product_id,
    DENSE_RANK() OVER(PARTITION BY s.customer_id ORDER BY s.order_date) AS rank
  FROM dannys_diner.sales AS s
	JOIN dannys_diner.members AS m
		ON s.customer_id = m.customer_id
	WHERE s.order_date >= m.join_date)
)
SELECT 
  s.customer_id, 
  s.order_date, 
  m2.product_name 
FROM member_sales_cte AS s
JOIN dannys_diner.menu AS m2
	ON s.product_id = m2.product_id
WHERE rank = 1;

--7. Which item was purchased just before the customer became a member?
WITH prior_member_purchased_cte AS 
(
  SELECT 
    s.customer_id, 
    m.join_date, 
    s.order_date, 
    s.product_id,
    DENSE_RANK() OVER(PARTITION BY s.customer_id ORDER BY s.order_date DESC)  AS rank
  FROM dannys_diner.sales AS s
	JOIN dannys_diner.members AS m
		ON s.customer_id = m.customer_id
	WHERE s.order_date < m.join_date
)

SELECT 
  s.customer_id, 
  s.order_date, 
  m2.product_name 
FROM prior_member_purchased_cte AS s
JOIN dannys_diner.menu AS m2
	ON s.product_id = m2.product_id
WHERE rank = 1;

--8. What is the total items and amount spent for each member before they became a member?
SELECT 
  s.customer_id, 
  COUNT(DISTINCT s.product_id) AS unique_menu_item, 
  SUM(mm.price) AS total_sales
FROM dannys_diner.sales AS s
JOIN dannys_diner.members AS m
	ON s.customer_id = m.customer_id
JOIN dannys_diner.menu AS mm
	ON s.product_id = mm.product_id
WHERE s.order_date < m.join_date
GROUP BY s.customer_id;

--9. If each $1 spent equates to 10 points and sushi has a 2x points multiplier - how many points would each customer have?
WITH price_points_cte AS
(
	SELECT *, 
		CASE WHEN product_name = 'sushi' THEN price * 20
		ELSE price * 10 END AS points
	FROM dannys_diner.menu
)

SELECT 
  s.customer_id, 
  SUM(p.points) AS total_points
FROM price_points_cte AS p
JOIN dannys_diner.sales AS s
	ON p.product_id = s.product_id
GROUP BY s.customer_id;

--10. In the first week after a customer joins the program (including their join date) they earn 2x points on all items, not just sushi - how many points do customer A and B have at the end of January?
-- 1. Find member validity date of each customer and get last date of January
-- 2. Use CASE WHEN to allocate points by date and product id
-- 3. SUM price and points

WITH dates_cte AS 
(
	SELECT 
    *, 
    DATEADD(DAY, 6, join_date) AS valid_date, 
		EOMONTH('2021-01-31') AS last_date
	FROM dannys_diner.members AS m
)

SELECT 
  d.customer_id, 
  s.order_date, 
  d.join_date, 
  d.valid_date, 
  d.last_date, 
  m.product_name, 
  m.price,
	SUM( 
    CASE WHEN m.product_name = 'sushi' THEN 2 * 10 * m.price
		WHEN s.order_date BETWEEN d.join_date AND d.valid_date THEN 2 * 10 * m.price
		ELSE 10 * m.price END) AS points
FROM dates_cte AS d
JOIN dannys_diner.sales AS s
	ON d.customer_id = s.customer_id
JOIN dannys_diner.menu AS m
	ON s.product_id = m.product_id
WHERE s.order_date < d.last_date
GROUP BY d.customer_id, s.order_date, d.join_date, d.valid_date, d.last_date, m.product_name, m.price

------------------------
--BONUS QUESTIONS-------
------------------------

-- Join All The Things
-- Recreate the table with: customer_id, order_date, product_name, price, member (Y/N)

SELECT 
  s.customer_id, 
  s.order_date, 
  m.product_name, 
  m.price,
  CASE WHEN mm.join_date > s.order_date THEN 'N'
	  WHEN mm.join_date <= s.order_date THEN 'Y'
	  ELSE 'N' END AS member
FROM dannys_diner.sales AS s
LEFT JOIN dannys_diner.menu AS m
	ON s.product_id = m.product_id
LEFT JOIN dannys_diner.members AS mm
	ON s.customer_id = mm.customer_id
ORDER BY s.customer_id, s.order_date

-- Rank All The Things
-- Recreate the table with: customer_id, order_date, product_name, price, member (Y/N), ranking(null/123)

WITH summary_cte AS 
(
  SELECT 
    s.customer_id, 
    s.order_date, 
    m.product_name, 
    m.price,
    CASE WHEN mm.join_date > s.order_date THEN 'N'
	    WHEN mm.join_date <= s.order_date THEN 'Y'
	    ELSE 'N'END AS member
FROM dannys_diner.sales AS s
LEFT JOIN dannys_diner.menu AS m
	ON s.product_id = m.product_id
LEFT JOIN dannys_diner.members AS mm
	ON s.customer_id = mm.customer_id
)

SELECT 
  *,
	CASE WHEN member = 'N' then NULL
    ELSE
			RANK () OVER(PARTITION BY customer_id, member ORDER BY order_date) 
		END AS ranking
FROM summary_cte;

--------------------------------
--------------------------------

