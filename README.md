# hotel-management

# Docker Commands

# Database

docker run -p 5432:5432 --name postgresdb -e POSTGRES_PASSWORD=admin -d postgres

# DB Client

docker run -p 5050:80 -e "PGADMIN_DEFAULT_EMAIL=dev@email.com" -e "PGADMIN_DEFAULT_PASSWORD=dev" -d dpage/pgadmin4
