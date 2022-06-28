## Migrations

Run this command in the Database directory: dotnet ef migrations add {MigrationName}

## Database update

Run this command in the WebService application directory:
dotnet ef database update

# Sql

## MongoDB

https://www.mongodb.com/docs/drivers/csharp/
https://www.mongodb.com/docs/manual/reference/connection-string/

### Run mongoDb (mac)

brew services start mongodb-community@5.0

mongod --port 27017 --dbpath /opt/homebrew/var/mongodb --replSet rs0 --config /opt/homebrew/etc/mongod.conf --fork

https://www.mongodb.com/docs/manual/tutorial/deploy-replica-set/
run in mongosh -> rs.initiate()

### Conversion from standalone to ReplicaSet

https://www.mongodb.com/docs/manual/tutorial/convert-standalone-to-replica-set/?_ga=2.153503675.2040640781.1656102647-1107091300.1656102646&_gac=1.90375656.1656158790.CjwKCAjw5NqVBhAjEiwAeCa97dUkLY_TqIAPJnMwcfftkS7aUAO0G4UaiV8nHoWkCiXicIAU2iUAhBoCc7QQAvD_BwE