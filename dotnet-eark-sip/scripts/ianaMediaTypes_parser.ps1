$RESOURCES_FOLDER = ".\src\Resources\ControlledVocabularies"
$EXTENDED_LIST = ".\scripts\ExtendedIANAMediaTypes.txt"

# Remove the file if it exists
Remove-Item -Path "$RESOURCES_FOLDER\IANA_MEDIA_TYPES.txt" -Force -ErrorAction SilentlyContinue

# Define the IANA groups and the missing MIME types
$ianaGroups = @("application", "audio", "font", "image", "message", "model", "multipart", "text", "video")
$missingMimeTypes = Get-Content -Path $EXTENDED_LIST

# Loop through each IANA group
foreach ($ianaGroup in $ianaGroups) {
    Write-Host $ianaGroup

    $downloadURL = "https://www.iana.org/assignments/media-types/$ianaGroup.csv"

    # Download the CSV, remove the header, filter out columns, and append to the file
    $csvData = Invoke-WebRequest -Uri $downloadURL | Select-Object -ExpandProperty Content
    $csvData | ConvertFrom-Csv | Select-Object -Skip 1 | ForEach-Object {
        "$($_.'Media Type'),$($_.'Subcategory')" 
    } | Where-Object { $_ -ne "" } | Out-File -Append -FilePath "$RESOURCES_FOLDER\IANA_MEDIA_TYPES.txt"
}

# Loop through each MIME type in the extended list and add it if it's missing
foreach ($mimeType in $missingMimeTypes) {
    $matchCount = (Select-String -Pattern "$mimeType$" -Path "$RESOURCES_FOLDER\IANA_MEDIA_TYPES.txt").Count
    if ($matchCount -eq 0) {
        $mimeType | Out-File -Append -FilePath "$RESOURCES_FOLDER\IANA_MEDIA_TYPES.txt"
    }
}
