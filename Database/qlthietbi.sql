use master
if exists(select * from sysdatabases where name='qlthietbi')
	drop database qlthietbi
go
create database qlthietbi
go
use qlthietbi
go 

create table tbl_users (
  user_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  username VARCHAR(255) NOT NULL,
  password VARCHAR(255) NOT NULL,
  role INT NOT NULL,
  status INT NOT NULL
);

create table tbl_roles (
  role_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  name_role NVARCHAR(255) NOT NULL,
  description NVARCHAR(255) NOT NULL,
);

create table tbl_locations (
  location_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  name_location NVARCHAR(255) NOT NULL,
  description NVARCHAR(255) NOT NULL,
  type VARCHAR(255) NOT NULL,
);

CREATE TABLE tbl_employees (
  employee_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  name NVARCHAR(255) NOT NULL,
  address NVARCHAR(255) NOT NULL,
  phone_number NVARCHAR(255) NOT NULL,
  email NVARCHAR(255) NOT NULL,
  location_id INT NOT NULL,
  role_id INT NOT NULL,
  FOREIGN KEY (location_id) REFERENCES tbl_locations (location_id),
  FOREIGN KEY (role_id) REFERENCES tbl_roles (role_id)
);

create table tbl_categories (
  category_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  name_category NVARCHAR(255) NOT NULL,
  description NVARCHAR(255) NOT NULL,
);

create table tbl_products (
  product_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  category_ID INT NOT NULL,
  name NVARCHAR(255) NOT NULL,
  model NVARCHAR(255) NOT NULL,
  manufacturer VARCHAR(255) NOT NULL,
  serial_number VARCHAR(255) NOT NULL,
  purchase_date DATETIME NOT NULL,
  warranty_end_date DATETIME NOT NULL,
  status INT NOT NULL,
  location_ID INT NOT NULL,
  FOREIGN KEY (category_ID) REFERENCES tbl_categories (category_id),
  FOREIGN KEY (location_ID) REFERENCES tbl_locations (location_id) ON DELETE CASCADE --mỗi sản phẩm chỉ có thể được đặt trong một địa điểm duy nhất
);


create table tbl_components (
  component_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  product_ID INT NOT NULL,
  name NVARCHAR(255) NOT NULL,
  manufacturer VARCHAR(255) NOT NULL,
  serial_number VARCHAR(255) NOT NULL,
  DVT NVARCHAR(255) NOT NULL,
  FOREIGN KEY (product_id) REFERENCES tbl_products (product_id)
);

create table tbl_orders (
  order_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  user_ID INT NOT NULL,
  product_ID INT NOT NULL,
  quantity INT NOT NULL,
  order_date DATETIME NOT NULL,
  status INT NOT NULL,
  FOREIGN KEY (user_ID) REFERENCES tbl_users (user_id),
  FOREIGN KEY (product_ID) REFERENCES tbl_products (product_id)
);

create table tbl_borrowings (
  borrowing_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  user_ID INT NOT NULL,
  product_ID INT NOT NULL,
  borrow_date DATETIME NOT NULL,
  return_date DATETIME NOT NULL,
  status INT NOT NULL,
  FOREIGN KEY (user_ID) REFERENCES tbl_users (user_id),
  FOREIGN KEY (product_ID) REFERENCES tbl_products (product_id)
);

create table tbl_maintenances (
  maintenance_id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
  product_ID INT NOT NULL,
  maintenance_date DATETIME NOT NULL,
  description NVARCHAR(255) NOT NULL,
  status INT NOT NULL,
  assign_to NVARCHAR(255) NOT NULL,
  completion_date DATETIME NOT NULL,
  FOREIGN KEY (product_ID) REFERENCES tbl_products (product_id)
);
INSERT INTO tbl_locations (name_location, description, type)
VALUES
(N'Kho trung tâm', N'Kho chứa thiết bị chính', N'Kho'),
(N'Cửa hàng A', N'Cửa hàng bán lẻ thiết bị', N'Cửa hàng'),
(N'Văn phòng công ty', N'Văn phòng làm việc của công ty', N'Cửa hàng'),
(N'Phòng bảo trì', N'Phòng bảo trì thiết bị', N'Cửa hàng'),
(N'Kho lưu trữ', N'Kho lưu trữ thiết bị cũ', N'Kho'),
(N'Xưởng sản xuất', N'Xưởng sản xuất thiết bị', N'Cửa hàng'),
(N'Cửa hàng B', N'Cửa hàng bán lẻ thiết bị', N'Cửa hàng');

INSERT INTO tbl_categories (name_category, description)
VALUES
(N'Máy tính xách tay', N'Các loại máy tính xách tay'),
(N'Máy tính để bàn', N'Các loại máy tính để bàn'),
(N'Màn hình', N'Các loại màn hình máy tính'),
(N'Bàn phím', N'Các loại bàn phím máy tính'),
(N'Chuột', N'Các loại chuột máy tính'),
(N'Tai nghe', N'Các loại tai nghe máy tính'),
(N'Loa', N'Các loại loa máy tính'),
(N'Webcam', N'Các loại webcam máy tính'),
(N'Máy in', N'Các loại máy in'),
(N'Máy chiếu', N'Các loại máy chiếu');

INSERT INTO tbl_products (category_ID, name, model, manufacturer, serial_number, purchase_date, warranty_end_date, status, location_ID)
VALUES
(1, N'Laptop Dell XPS 13', '9310', 'Dell', 'ABCD1234EFGH5678', '2023-01-01', '2025-01-01', 1, 1),
(1, N'Laptop HP Envy 13', '13-ba1020TU', 'HP', 'HIJKLMNOPQRSTUVWXYZ1234', '2023-02-01', '2025-02-01', 1, 2),
(1, N'Laptop Acer Aspire 5', 'A515-56-356C', 'Acer', 'MNOPQRSTUVWX1234', '2023-03-01', '2025-03-01', 1, 3),
(2, N'PC Dell OptiPlex 7090', 'MT54R', 'Dell', 'PQRSTUVWXYZ1234', '2023-04-01', '2025-04-01', 1, 1),
(2, N'PC HP ProDesk 400 G8', 'MT842', 'HP', 'TUVWXYZ1234ABCD', '2023-05-01', '2025-05-01', 1, 2),
(2, N'PC Lenovo ThinkCentre M75q Gen 2', '11FH002TUS', 'Lenovo', 'VWXYZ1234ABCDE', '2023-06-01', '2025-06-01', 1, 3),
(3, N'Màn hình Dell UltraSharp 27 4K', 'U2723QE', 'Dell', '1234ABCDE567890', '2023-07-01', '2025-07-01', 1, 1),
(3, N'Màn hình LG UltraFine 32 4K', '32UN850-W', 'LG', '9876543210ABCDE', '2023-08-01', '2025-08-01', 1, 2),
(3, N'Màn hình Samsung Odyssey G9', 'LC49G97T-QD', 'Samsung', '7654321098ABCDE', '2023-09-01', '2025-09-01', 1, 3),
(4, N'Bàn phím Logitech K380', '920-007558', 'Logitech', '5432109876ABCDE', '2023-10-01', '2024-10-01', 1, 1),
(5, N'Chuột Logitech M590', '910-005156', 'Logitech', '3210987654ABCDE', '2023-11-01', '2024-11-01', 1, 2),
(6, N'Tai nghe Logitech G333', '9876543210ABCDE', 'Logitech', '1234ABCDE567890', '2024-06-01', '2026-06-01', 1, 1),
(7, N'Loa Bluetooth Logitech Z407', '7654321098ABCDE', 'Logitech', '9876543210ABCDE', '2024-07-01', '2026-07-01', 1, 2),
(8, N'Webcam Logitech C920', '3210987654ABCDE', 'Logitech', '7654321098ABCDE', '2024-08-01', '2026-08-01', 1, 3),
(9, N'Máy in HP LaserJet Pro M140w', '1234567890ABCDE', 'HP', '5432109876ABCDE', '2024-09-01', '2026-09-01', 1, 1),
(10, N'Máy chiếu BenQ W1070', '9876543210ABCDE', 'BenQ', '3210987654ABCDE', '2024-10-01', '2026-10-01', 1, 2),
(9, N'Máy in Canon PIXMA G3260', '7654321098ABCDE', 'Canon', '1234567890ABCDE', '2024-11-01', '2026-11-01', 1, 3),
(10, N'Máy chiếu Optoma HD27e', '5432109876ABCDE', 'Optoma', '9876543210ABCDE', '2024-12-01', '2026-12-01', 1, 1),
(7, N'Loa soundbar LG SN11R', '3210987654ABCDE', 'LG', '7654321098ABCDE', '2025-01-01', '2027-01-01', 1, 2),
(9, N'Máy in đa năng Brother MFC-L8900DW', '1234567890ABCDE', 'Brother', '5432109876ABCDE', '2025-02-01', '2027-02-01', 1, 3);

INSERT INTO tbl_components (product_id, name, manufacturer, serial_number, DVT)
VALUES
(1, N'Bộ vi xử lý Intel Core i7-12700H', 'Intel', '1234567890123456', 'Cái'),
(1, N'Bộ nhớ RAM 16GB DDR5', 'Samsung', '9876543210987654', 'Chiếc'),
(1, N'Ổ cứng SSD 512GB M.2 NVMe', 'Samsung', '3210987654321098', 'Cái'),
(2, N'Bộ vi xử lý Intel Core i5-12400F', 'Intel', '6543210987654321', 'Cái'),
(2, N'Bộ nhớ RAM 8GB DDR4', 'Kingston', '5432109876543210', 'Chiếc'),
(2, N'Ổ cứng HDD 1TB 2.5" SATA', 'Seagate', '4321098765432109', 'Cái'),
(3, N'Màn hình Dell UltraSharp 27 4K', 'Dell', '1234ABCDE567890', 'Cái'),
(4, N'Bàn phím Logitech K380', 'Logitech', '5432109876ABCDE', 'Chiếc'),
(4, N'Chuột Logitech M590', 'Logitech', '3210987654ABCDE', 'Chiếc'),
(1, N'Bộ nguồn Dell DA-220PM', 'Dell', '1234567890123456', 'Cái'),
(1, N'Pin Dell XPS 13 9310', 'Dell', '9876543210987654', 'Cái'),
(2, N'Bộ nguồn HP 180W', 'HP', '6543210987654321', 'Cái'),
(2, N'Pin HP Envy 13-ba1020TU', 'Kingston', '5432109876543210', 'Cái'),
(3, N'Cổng chuyển đổi Dell USB-C to HDMI', 'Dell', '4321098765432109', 'Cái'),
(4, N'Tai nghe Logitech G333', 'Logitech', '1234ABCDE567890', 'Chiếc'),
(4, N'Loa Bluetooth Logitech Z407', 'Logitech', '3210987654ABCDE', 'Chiếc'),
(1, N'Webcam Logitech C920', 'Logitech', '1234567890123456', 'Chiếc'),
(1, N'Máy in HP LaserJet Pro M140w', 'HP', '9876543210987654', 'Cái'),
(1, N'Máy chiếu BenQ W1070', 'BenQ', '6543210987654321', 'Cái'),
(2, N'Máy in Canon PIXMA G3260', 'Canon', '5432109876543210', 'Cái');


SELECT * FROM tbl_components
SELECT * FROM tbl_users
INSERT INTO tbl_roles (name_role, description)
VALUES 
(N'Quản trị viên hệ thống', N'Quản trị viên cho toàn hệ thống'),
(N'Quản trị viên kho', N'Quản trị viên của kho'),
(N'Nhân viên kho', N'Nhân viên của kho'),
(N'Quản trị viên cửa hàng', N'Quản trị viên của cửa hàng'),
(N'Nhân viên cửa hàng', N'Nhân viên của cửa hàng');

INSERT INTO tbl_users (username, password, role, status) VALUES
    ('admin', '123456', 1, 1),
    ('kho', 'kho123456', 2, 1),
    ('nhanvienkho', 'nvkho123456', 3, 1),
    ('cuahang', 'cuahang123456', 4, 1),
    ('nhanviencuahang', 'nvch123456', 5, 1);

INSERT INTO tbl_borrowings (user_ID, product_ID, borrow_date, return_date, status) VALUES
	(1, 2, '2024-03-20', '2024-04-10', 1),
	(3, 1, '2024-03-25', '2024-04-15', 1),
	(2, 4, '2024-03-12', '2024-03-22', 2),
	(1, 3, '2024-03-08', '2024-03-18', 2),
	(3, 5, '2024-03-30', '2024-05-10', 1);

INSERT INTO tbl_employees (name, address, phone_number, email, location_id, role_id)
VALUES
(N'Từ La Thanh Hoàng', N'373 Lý Thường Kiệt, Quận Tân Bình, TP.HCM', '1234567891', 'thanhhoang281202@gmail.com', 3, 1),
(N'Nguyễn Văn An', N'132 Đường 3/2, Quận 10, TP.HCM', '0123456789', 'nguyenvana@example.com', 1, 2),
(N'Trần Thị Bình', N'723 Đường Lý Thường Kiệt, Quận Tân Bình, TP.HCM', '0987654321', 'tranthib@example.com', 2, 3),
(N'Phạm Văn Cường', N'145 Đường Lý Thường Kiệt, Quận Tân Bình, Thành phố Đà Lạt', '0123987654', 'phamvanc@example.com', 3, 4),
(N'Lê Thị Mai', N'320 Đường Nguyễn Văn Cừ	, Quận Ninh Kiều, Thành phố Cần Thơ', '0987123456', 'lethid@example.com', 4, 5);