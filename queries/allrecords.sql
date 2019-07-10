SELECT records.recordID, records.Name, Bands.Name as 'Artist', genres.name as 'Genre' FROM recordcollection.records
JOIN Bands on Bands.bandid= records.bandid
JOIN Genres on records.genreid= genres.genreid
ORDER BY bands.bandID DESC, records.year
LIMIT 128;