FROM defradigital/dotnetcore-development:dotnet6.0 AS build-env

ARG PORT=3000
ENV ASPNETCORE_URLS=http://*:${PORT}
EXPOSE ${PORT}

COPY --chown=dotnet ["EPR.Accreditation.Facade/EPR.Accreditation.Facade.csproj", "EPR.Accreditation.Facade/"]
COPY --chown=dotnet  ["EPR.Accreditation.Facade.Common/EPR.Accreditation.Facade.Common.csproj", "EPR.Accreditation.Facade.Common/"]

RUN dotnet restore "EPR.Accreditation.Facade/EPR.Accreditation.Facade.csproj"

USER root
RUN apk update && apk add --no-cache npm
USER dotnet
ENV NODE_EXTRA_CA_CERTS=/usr/local/share/ca-certificates/internal-ca.crt

COPY --chown=dotnet:dotnet . .
WORKDIR /home/dotnet/EPR.Accreditation.Facade
RUN dotnet publish -c Release -o out

FROM defradigital/dotnetcore-development:dotnet6.0
COPY --from=build-env --chown=dotnet /home/dotnet/EPR.Accreditation.Facade/out .

USER root
RUN apk add icu-libs libintl
USER dotnet

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0

ARG PORT=3000
ENV ASPNETCORE_URLS=http://*:${PORT}
EXPOSE ${PORT}

CMD dotnet EPR.Accreditation.Facade.dll