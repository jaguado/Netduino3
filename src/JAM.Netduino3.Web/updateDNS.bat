@echo off
for /f "skip=1 delims={}, " %%A in ('wmic nicconfig get ipaddress') do for /f "tokens=1" %%B in ("%%~A") do set "IP=%%~B"
curl -X PUT "https://api.cloudflare.com/client/v4/zones/fb19a96a40272eafeb5af2cdc2e50c36/dns_records/14c49a630847c6e39332926c1f770af7"     -H "X-Auth-Email: jorge@jamtech.cl"     -H "X-Auth-Key: 9d09bacc66bf699b397f4204ae45d5b841be2"     -H "Content-Type: application/json"     --data "{\"type\":\"A\",\"name\":\"iot.jamtech.cl\",\"content\":\"%IP%\",\"ttl\":120,\"proxied\":false}"

pause