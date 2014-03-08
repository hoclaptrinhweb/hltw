@echo off
echo Chay dang ky Cache
pause
cd C:\Windows\Microsoft.NET\Framework\v4.0.30319
aspnet_regsql.exe -S VONHATNAM-PC\SQLServerR2 -U sa -P vnn123456 -d HocLapTrinhWeb.com -ed
aspnet_regsql.exe -S VONHATNAM-PC\SQLServerR2 -U sa -P vnn123456 -d HocLapTrinhWeb.com -t tbl_NewsType -et
aspnet_regsql.exe -S VONHATNAM-PC\SQLServerR2 -U sa -P vnn123456 -d HocLapTrinhWeb.com -t tbl_News -et
aspnet_regsql.exe -S VONHATNAM-PC\SQLServerR2 -U sa -P vnn123456 -d HocLapTrinhWeb.com -lt
PAUSE
