USE IGB_FAR;


CREATE TABLE GeneralAssets
(
	IdtAsset int IDENTITY(1,1) PRIMARY KEY,
	IdtTrack int NOT NULL,
	IdtDeviceType int NOT NULL,
	IdtManufacture int NOT NULL,
	IdtOS int NOT NULL,
	IdtDepartment int NOT NULL,
	Model VARCHAR(30),
	Description VARCHAR(100),
	Hostname VARCHAR(30),
	IPAddress VARCHAR(20),
	ServiceTag VARCHAR(20),
	ExpressCode VARCHAR(20),
	ShipDate datetime,
	Comments VARCHAR(255) NULL,
	Inactive bit,
	foreign key (IdtTrack) references LookupTrack (IdtTrack),
	foreign key (IdtDeviceType) references LookupDeviceType (IdtDeviceType),
	foreign key (IdtManufacture) references LookupManufacture (IdtManufacture),
	foreign key (IdtOS) references LookupOS (IdtOS),
	foreign key (IdtDepartment) references LookupDepartment (IdtDepartment)
)