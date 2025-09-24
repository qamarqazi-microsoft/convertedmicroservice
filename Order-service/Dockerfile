FROM openjdk:17-jdk-alpine
VOLUME /tmp
COPY target/order-service.jar order-service.jar
EXPOSE 8080
ENTRYPOINT ["java","-jar","/order-service.jar"]
