# PanoramaPOC
POC

## Configuracion de base de datos

El proyecto utiliza un bloque `Database` en `appsettings.json` para definir la
conexion a PostgreSQL. Estos valores pueden sobrescribirse mediante variables de
entorno con el prefijo `Database__`.

Ejemplo de configuracion de produccion:

```json
"Database": {
  "ConnectionString": "postgresql://postgres:[YOUR-PASSWORD]@db.mnkupvkwdsfszvytucst.supabase.co:5432/postgres",
  "Host": "db.mnkupvkwdsfszvytucst.supabase.co",
  "Port": 5432,
  "Database": "postgres",
  "User": "postgres",
  "Password": "[YOUR-PASSWORD]"
}
```

Para configurarlo mediante variables de entorno, defina por ejemplo:

```bash
export Database__Password=mi_password
```

De esta manera la configuracion es facilmente modificable segun el entorno.
