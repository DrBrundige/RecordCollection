SELECT songs.SongId, songs.Name, songs.TrackListing, songs.score, records.name as 'Record Name', bands.name as 'Band Name'
FROM recordcollection.songs
JOIN records on songs.RecordId = records.recordID
JOIN bands on records.bandid = bands.bandid