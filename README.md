- To run application you need to pull code from this github page
- Install if you don't have yet installed Docker Desktop and than run it. Link for download -https://www.docker.com/products/docker-desktop/
- Run docker compose up --build from root folder
  #VISUAL STUDIO RUN
- To run from VS, you will ned to have SQL server installed locally and to update connection string to database inside appsettings.json or 4CreateWebApiJsonUpload

POTENTIAL TROUBLESHOOTING

Out of problems I had during running Docker I can point out next ones: - First time i got hands on my docker so I was never sure If I was writing something bad in Docker or my local env was problem
- Had to open port for any port I would run docker database on in order to be able to hit it
- Had all kind of handshake issues during accessing SQL server by Docker so I was constantly switching between sql 2022 and sql 2019
- I was changing sql version in order to try to run it and in the end I encountered sql 2022 was not working for me (while 2019 was working)
- Meanwhile I was changing ports as well, so for some reason port 1401 was not working while 1433 was working
- When I finally worked out how to run DB from SSMS, than I had problem connecting to it from VS when running from there by adding series off properties on Local connection string
- After I solved that issue by adopting Connection string, than I had to to solve issue connecting to DB in DockerFile by changing url from localhost,1433 to appdb
