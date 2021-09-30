# productservice
https://code.visualstudio.com/docs/containers/quickstart-aspnet-core

#Manually add your connection string
```code
kubectl create secret generic dbnorthwindconnection -n incomm-poc --from-literal=connectionstring='mssql-mssql-linux.mssql,1433;Database=Northwind;User
 Id=sa;Password=********;MultipleActiveResultSets=true'
 ```

```code
kubectl create secret generic dbnorthwindconnection -n incomm-poc --from-literal=connectionstring='Server=mssql.kaiser.local;Database=Northwind;User Id=sa;Password=********;MultipleActiveResultSets=true'
```
