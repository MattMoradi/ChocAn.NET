# ChocAn.NET
A .NET implementation of the Chocoholics Anonymous Terminal System v1.0

## Database
Instantiating a database instance:
`Database database = new Database();`

Setting generic values in the database:
`database.members[index].zip = 1234;` `database.providers[index].date = "02-20-2022";`

Setting a member validation status (0-2):
`database.members[index].status = (Database.Validity)1;`

Using the records subarrays (for each index):
```
database.providers[index].record = new Database.ProviderRecords[999];
database.providers[index].records[index2].date = "02-20-2022";
```

Setting timestamps manually:
`database.providers[index].records[index].timestamp = DateTime.Parse("02-20-2022 04:20:22");`

Setting timestamps automatically:
`database.providers[index].records[index].timestamp = DateTime.Now;`

Writing 2 Disk:
`database.save2disk(database);`
`database.writeEFT(database);`