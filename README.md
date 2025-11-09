database______________
CREATE TABLE product_type (
    product_type_id SERIAL PRIMARY KEY,
    type_name VARCHAR(100) NOT NULL UNIQUE,
    coefficient DECIMAL(8,2) NOT NULL
);
CREATE TABLE product (
    product_id SERIAL PRIMARY KEY,
    product_type_id_fk INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    sku VARCHAR(50) NOT NULL UNIQUE,
    min_partner_price DECIMAL(8,2) NOT NULL,
    CONSTRAINT fk_product_type FOREIGN KEY (product_type_id_fk) REFERENCES product_type(product_type_id)
);
CREATE TABLE material_type (
    material_id SERIAL PRIMARY KEY,
    material_name VARCHAR(100) NOT NULL UNIQUE,
    defect_percent DECIMAL(4,4) NOT NULL
);
CREATE TABLE partners (
    partner_id SERIAL PRIMARY KEY,
    partner_type VARCHAR(50) NOT NULL,
    name VARCHAR(150) NOT NULL,
    director VARCHAR(150) NOT NULL,
    email VARCHAR(100) NOT NULL,
    phone VARCHAR(20),
    address VARCHAR(255),
    inn VARCHAR(12) NOT NULL UNIQUE,
    rating INT CHECK (rating BETWEEN 0 AND 10)
);
CREATE TABLE partner_products (
    partner_product_id SERIAL PRIMARY KEY,
    product_id_fk INT NOT NULL,
    partner_id_fk INT NOT NULL,
    quantity INT NOT NULL,
    start_date DATE NOT NULL DEFAULT CURRENT_DATE,
    CONSTRAINT fk_partner FOREIGN KEY (partner_id_fk) REFERENCES partners(partner_id),
    CONSTRAINT fk_product FOREIGN KEY (product_id_fk) REFERENCES product(product_id)
);

-- 6. Временная таблица для загрузки данных о продажах (partner_products_import.xlsx)
-- Нужно будет очистить после загрузки
CREATE TABLE IF NOT EXISTS public.partner_products_csv (
    product_sku VARCHAR(150),
    partner_name VARCHAR(150),
    quantity INT,
    start_date DATE
);

-- 7. Временная таблица для загрузки данных о продуктах (Products_import.xlsx)
-- Нужно будет очистить после загрузки
CREATE TABLE IF NOT EXISTS public.product_import_csv (
    product_type_name VARCHAR(100),
    name VARCHAR(255),
    sku VARCHAR(50),
    min_partner_price NUMERIC(8,2)
);

---работает
INSERT INTO product (product_type_id_fk, name, sku, min_partner_price)
SELECT pt.product_type_id, c.name, c.sku, c.min_partner_price
FROM product_import_csv c
JOIN product_type pt ON pt.type_name = c.product_type_name;

INSERT INTO partner_products (product_id_fk, partner_id_fk, quantity, start_date)
SELECT pr.product_id, p.partner_id, c.quantity, COALESCE(c.start_date, CURRENT_DATE)
FROM partner_products_csv c
JOIN product pr ON trim(pr.name) = trim(c.product_sku)  -- здесь name вместо sku
JOIN partners p ON trim(lower(p.name)) = trim(lower(c.partner_name));
