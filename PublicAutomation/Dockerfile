# Use Microsoft's official build .NET image.
FROM mcr.microsoft.com/dotnet/core/sdk:3.1

RUN df -h -k /dev/shm

RUN apt-get update && apt-get install -y apt-utils && \
    apt-get install -y curl unzip xvfb libxi6 libgconf-2-4 && \
    apt-get install -y default-jdk

RUN wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
RUN apt install -y ./google-chrome-stable_current_amd64.deb


RUN apt install default-jdk

# install mono so we can get specrun reports, not sure it matters. If removed turn off reporting in srprofile.
RUN apt install gnupg ca-certificates
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
RUN echo "deb https://download.mono-project.com/repo/ubuntu stable-focal main" | tee /etc/apt/sources.list.d/mono-official-stable.list
RUN apt update

WORKDIR /src
COPY *.csproj .
RUN dotnet restore
COPY . .

RUN dotnet publish -c Release -o /app
 
WORKDIR /app

EXPOSE 4442
EXPOSE 4443
EXPOSE 4444


# start the tests allowing for cmd line arguments
ENTRYPOINT ["dotnet", "test", "PublicAutomation.dll", "-v", "n"]