USE IGB_FAR;

GO

CREATE TABLE LookupTrack
(
	IdtTrack int IDENTITY(1,1) PRIMARY KEY,
	NamTrack varchar(30) NOT NULL,
	IPSubnet varchar(20) NULL
)

GO

CREATE TABLE LookupDeviceType
(
	IdtDeviceType int IDENTITY(1,1) PRIMARY KEY,
	NamDeviceType varchar(30) NOT NULL
)

GO

CREATE TABLE LookupManufacture
(
	IdtManufacture int IDENTITY(1,1) PRIMARY KEY,
	NamManufacture varchar(30) NOT NULL
)

GO

CREATE TABLE LookupOS
(
	IdtOS int IDENTITY(1,1) PRIMARY KEY,
	NamOS varchar(30) NOT NULL
)

GO

CREATE TABLE LookupDepartment
(
	IdtDepartment int IDENTITY(1,1) PRIMARY KEY,
	NamDepartment varchar(30) NOT NULL
)

GO

INSERT INTO LookupTrack (NamTrack, IPSubnet)
VALUES
('Clonmel', '172.17.96.#'),
('Cork', '172.17.24.#'),
('Dundalk', '172.17.48.#'),
('Enniscorthy', '172.17.68.#'),
('Galway', '172.17.44.#'),
('Harolds cross', '172.17.28.#'),
('HQ', '172.17.16.#'),
('Kilkenny', '172.17.52.#'),
('Lifford', '172.17.72.#'),
('Limerick', '172.17.16.#'),
('Longford', '172.17.76.#'),
('Mullingar', '172.17.60.#'),
('Newbridge', '172.17.80.#'),
('Shelbourne', '172.17.20.#'),
('Sales Centre', '172.17.100.#'),
('Thurles', '172.17.56.#'),
('Tralee', '172.17.32.#'),
('Waterford', '172.17.40.#'),
('Youghal', '172.17.64.#')

GO

INSERT INTO LookupDeviceType (NamDeviceType)
VALUES
('Desktop'),
('Laptop'),
('Printer')

GO

INSERT INTO LookupManufacture (NamManufacture)
VALUES
('Dell'),
('HP'),
('Nortel')

GO

INSERT INTO LookupOS (NamOS)
VALUES
('Printer'),
('Windows 7 64'),
('Windows 7 32'),
('Windows XP'),
('Windows 8')

GO

INSERT INTO LookupDepartment (NamDepartment)
VALUES
('Admin'),
('Regulation'),
('Finance'),
('EHS'),
('IT'),
('Tote'),
('Marketing'),
('Sales'),
('HR'),
('General')