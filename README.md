# BlazorWasmAppDemo

Proyecto creado mediante:
```console
> dotnet new blazorwasm -o BlazorAppDemo
```

Nginx file configuration
```nginx.conf
events {}
http {
    include /etc/nginx/mime.types;
    server {
        listen 8080;
        server_name localhost;
        root /usr/share/nginx/html;
        index index.html;
        location / {
            try_files $uri $uri/ /index.html;
        }
    }
}
```

Change:
COPY ./BlazorAppDemo.csproj ./
To
COPY ./BlazorAppDemo/BlazortAppDemo.csproj ./BlazorAppDemo/

```Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app

COPY BlazorAppDemo.sln ./
COPY ./BlazorAppDemo.csproj ./

RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out


FROM nginx:1.23.0-alpine
WORKDIR /app
EXPOSE 8080
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /app/out/wwwroot /usr/share/nginx/html
```

To build image:
```console
> docker build -t blazorappdemo .
```

To run container
```console
> docker run --name blazorcontainerdemo -d -p 5002:8080 --rm blazorappdemo 
```