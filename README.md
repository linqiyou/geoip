# geoip
RESTful service for retrieving geolocations from IP addresses using ASP.NET Core.

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
```
http://geoip.tech/api/maxmind?ipaddress={ip-address}
```

### IP2Location
```
http://geoip.tech/api/ip2location?ipaddress={ip-address}
```

Make a HTTP GET request to the endpoints to retrieve the IP address geolocation. Replace {ip-address} with the IP address to be searched. 

## Usage Example
```bash
curl http://geoip.tech/api/maxmind?ipaddress=123.123.123.123
```

### Result Example
```javascript
{
    "ipaddress":"123.123.123.123",
    "city":"Beijing",
    "country":"China",
    "latitude":39.9289,
    "longitude":116.3883
}
```

### Error Example
```javascript
{
    "error_message":"Invalid IP address provided"
}
```
