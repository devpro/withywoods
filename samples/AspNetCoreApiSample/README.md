# ASP.NET Core REST API sample

## Docker build & debug

From the root directory of the repository:

```bash
# build a new image with
docker build . -t devpro/aspnetcore-sample -f samples\AspNetCoreApiSample\Dockerfile --no-cache
# create and run a container
docker run -d -p 8080:80 --name aspnetcoresample devpro/aspnetcore-sample
# if there is an issue (direct crash), replace the ENTRYPOINT line by CMD "/bin/bash" and run
docker run -i -t -p 8080:80 devpro/aspnetcore-sample
# open the Swagger page in the browser: http://localhost:8080/swagger
```
