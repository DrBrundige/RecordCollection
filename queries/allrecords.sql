SELECT records.recordID, records.Name, Bands.Name FROM recordcollection.records
JOIN Bands on Bands.bandid= records.bandid;