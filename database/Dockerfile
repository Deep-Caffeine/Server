FROM mariadb:lts

ARG DB_CONTAINER_USER
ARG DB_CONTAINER_GROUP

ENV LC_ALL C.UTF-8

RUN usermod -u 1000 ${DB_CONTAINER_USER} \
    && groupmod -g 1000 ${DB_CONTAINER_GROUP}

COPY my.cnf /etc/mysql/my.cnf