$base = "https://localhost:5001"

Write-Host "----- Single Anime View -----"
curl.exe -k "$base/api/animeviews/1" | jq

Write-Host "`n----- Recommendations -----"
curl.exe -k "$base/api/animeviews/1/recommendations" | jq

Write-Host "`n----- Stats -----"
curl.exe -k "$base/api/animeviews/1/stats" | jq

Write-Host "`n----- Search Cowboy -----"
curl.exe -k "$base/api/anime/search?title=Cowboy" | jq

Write-Host "`n----- Min Score 8.5 -----"
curl.exe -k "$base/api/anime/minScore?minScore=8.5" | jq
