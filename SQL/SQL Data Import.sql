use IGB_FAR
 
Truncate table AssetRegister;  
 
Insert Into AssetRegister (IdtTrack, 
							IdtDeviceType,
							IdtManufacture, 
							IdtOS,
							IdtDepartment,
							Model,
							Description,
							Hostname,
							IPAddress,
							ServiceTag,
							ExpressCode,
							ShipDate,
							Comments,
							Inactive)

select
  Track, DeviceType, Manufacture, OS, Department, [Make Model], Description, [Host Name], [PC IP Address],
  [Service TAG], [Express service code], [Ship Date], Comments, Inactive
  FROM IGB_STAGING..Assets A