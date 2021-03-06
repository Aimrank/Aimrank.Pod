# -- Step 1 -- Restore and build web API

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /app

COPY src/Aimrank.Pod.Api/*.csproj ./src/Aimrank.Pod.Api/
COPY src/Aimrank.Pod.Core/*.csproj ./src/Aimrank.Pod.Core/
COPY src/Aimrank.Pod.Infrastructure/*.csproj ./src/Aimrank.Pod.Infrastructure/

RUN dotnet restore src/Aimrank.Pod.Api

COPY src/Aimrank.Pod.Api/. ./src/Aimrank.Pod.Api/
COPY src/Aimrank.Pod.Core/. ./src/Aimrank.Pod.Core/
COPY src/Aimrank.Pod.Infrastructure/. ./src/Aimrank.Pod.Infrastructure/

RUN dotnet publish src/Aimrank.Pod.Api -c Release -o /app/out

# -- Step 2 -- Create image with web API and CS:GO server

FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN mkdir -p /home/app

COPY --from=build /app/out/ /home/app/.

ENV STEAM_DIR /home/steam
ENV STEAM_CMD_DIR /home/steam/steamcmd
ENV CSGO_APP_ID 740
ENV CSGO_DIR /home/steam/csgo

ARG STEAM_CMD_URL=https://steamcdn-a.akamaihd.net/client/installer/steamcmd_linux.tar.gz

RUN DEBIAN_FRONTEND=noninteractive && apt-get update \
  && apt-get install --no-install-recommends --no-install-suggests -y \
      lib32gcc1 \
      lib32stdc++6 \
      ca-certificates \
      net-tools \
      locales \
      curl \
      wget \
      unzip \
      screen \
  && locale-gen en_US.UTF-8 \
  && adduser --disabled-password --gecos "" steam \
  && mkdir ${STEAM_CMD_DIR} \
  && cd ${STEAM_CMD_DIR} \
  && curl -sSL ${STEAM_CMD_URL} | tar -zx -C ${STEAM_CMD_DIR} \
  && mkdir -p ${STEAM_DIR}/.steam/sdk32 \
  && ln -s ${STEAM_CMD_DIR}/linux32/steamclient.so ${STEAM_DIR}/.steam/sdk32/steamclient.so \
  && { \
    echo '@ShutdownOnFailedCommand 1'; \
    echo '@NoPromptForPassword 1'; \
    echo 'login anonymous'; \
    echo 'force_install_dir ${CSGO_DIR}'; \
    echo 'app_update ${CSGO_APP_ID}'; \
    echo 'quit'; \
  } > ${STEAM_DIR}/autoupdate_script.txt \
  && mkdir ${CSGO_DIR} \
  && chown -R steam:steam ${STEAM_DIR} \
  && rm -rf /var/lib/apt/lists/*
  
COPY --chown=steam:steam container_fs/csgo/ ${STEAM_DIR}/

# -- Step 3 -- Compile sourcemod plugins

WORKDIR ${STEAM_DIR}/sourcemod/plugins

RUN tar -xzf build.tar.gz \
  && chmod +x ./build/sourcemod/scripting/spcomp \
  && ./build/sourcemod/scripting/spcomp aimrank.sp
  
# -- Step 4 -- Startup

VOLUME ${CSGO_DIR}

WORKDIR /home/app

EXPOSE 30001-30004/udp
EXPOSE 30001-30004/tcp

HEALTHCHECK --interval=30s --timeout=30s --start-period=30s --retries=5 \
  CMD curl -f http://localhost/ || exit 1
  
ENV ASPNETCORE_ENVIRONMENT=Production
  
ENTRYPOINT ["dotnet", "Aimrank.Pod.Api.dll"]
