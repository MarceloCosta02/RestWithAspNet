FROM mysql:5.7.22
EXPOSE 3306
COPY ./WebApi-Persons/db/migrations/ /home/database/
COPY ./WebApi-Persons/db/dataset/ /home/database/
COPY ./WebApi-Persons/ci/init_database.sh /docker-entrypoint-initdb.d/init_database.sh