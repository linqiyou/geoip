# geoip
RESTful service for retrieving geolocations from IP addresses or domain names using ASP.NET Core.

Geolocation data provided by MaxMind GeoLite2 and IP2Location LITE databases.

IP2Location geolocation data retrieval by [geoip-webservice](https://github.com/zulhilmizainuddin/geoip-webservice).

## Getting Started
Install .NET Core. Follow the instruction at https://www.microsoft.com/net/core#ubuntu.

Install Node.js. Follow the instruction at https://www.digitalocean.com/community/tutorials/how-to-install-node-js-on-ubuntu-16-04.

Install Bower.
```bash
npm install -g bower
```

Install PM2.
```bash
npm install -g pm2
```

Download and place the MaxMind GeoLite2 City binary database under the Databases/MaxMind directory.
Download the database at http://dev.maxmind.com/geoip/geoip2/geolite2/.

Retrieve project dependencies.
```bash
dotnet restore
```

Retrieve Bower dependencies.
```bash
bower install
```

Start the service using PM2.
```bash
pm2 start --name geoip dotnet -- run
```

## Endpoints
### MaxMind

#### Search By IP Address
```
http://geoip.tech/api/maxmind?ipaddress={ip-address}
```

#### Search By Domain Name
```
http://geoip.tech/api/maxmind?domainname={domain-name}
```

### IP2Location
#### Search By IP Address
```
http://geoip.tech/api/ip2location?ipaddress={ip-address}
```

#### Search By Domain Name
```
http://geoip.tech/api/ip2location?domainname={domain-name}
```

Make a HTTP GET request to the endpoints to retrieve the IP address or domain name geolocation. Replace {ip-address} with the IP address or {domain-name} with the domain name to be searched. 

## Usage Example
#### Search By IP Address
```bash
curl http://geoip.tech/api/maxmind?ipaddress=192.30.253.113
```

#### Search By Domain Name
```bash
curl http://geoip.tech/api/maxmind?domainname=github.com
```

### Result Example
```javascript
{
    "ipaddress":"192.30.253.113",
    "city":"San Francisco",
    "country":"United States",
    "latitude":37.7697,
    "longitude":-122.3933
}
```

### Error Example
```javascript
{
    "error_message":"Invalid IP address provided"
}
```
