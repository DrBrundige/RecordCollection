SELECT musiciantorecord.MusicianToRecordId, musicians.name, records.name, rolls.name FROM musiciantorecord
JOIN musicians on musiciantorecord.MusicianId = musicians.MusicianId
JOIN rolls on musiciantorecord.RollId= rolls.RollId
JOIN records on musiciantorecord.RecordId = records.RecordId;