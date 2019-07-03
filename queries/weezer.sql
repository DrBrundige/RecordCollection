INSERT INTO cities (Name, state, country, createdAt, UpdatedAt) VALUES('Los Angeles', 'CA', 'America' now(), now());
INSERT INTO labels (Name, cityID, createdAt, UpdatedAt) VALUES('Geffen Records', 1, now(), now());
INSERT INTO bands (Name, cityID, createdAt, UpdatedAt) VALUES('Weezer', 1, now(), now());
INSERT INTO records (Name, bandID, year, domain, zeitgeist, createdAt, UpdatedAt) VALUES('Weezer', 1, 1994, 1, 2015, now(), now());

INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('My Name is Jonas', 1, 1, 85, now(), now());
INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('No One Else', 1, 2, 20, now(), now());
INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('The World has Turned', 1, 3, 85, now(), now());
INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('Buddy Holly', 1, 4, 70, now(), now());
INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('Undone', 1, 5, 100, now(), now());
INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('Surf Wax America', 1, 6, 30, now(), now());
INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('Say It Aint So', 1, 7, 100, now(), now());
INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('In the Garage', 1, 8, 85, now(), now());
INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('Holiday', 1, 9, 60, now(), now());
INSERT INTO songs (Name, recordID, trackListing, score, createdAt, UpdatedAt) VALUES('Only in Dreams', 1, 10, 75, now(), now());

INSERT INTO musicians (Name, createdAt, UpdatedAt) VALUES('Rivers Cuomo', now(), now());
INSERT INTO musicians (Name, createdAt, UpdatedAt) VALUES('Patrick Wilsom', now(), now());
INSERT INTO musicians (Name, createdAt, UpdatedAt) VALUES('Matt Sharp', now(), now());
INSERT INTO musicians (Name, createdAt, UpdatedAt) VALUES('Brian Bell', now(), now());
INSERT INTO musicians (Name, createdAt, UpdatedAt) VALUES('Ric Ocasek', now(), now())

INSERT INTO musiciantorecord (MusicianID, RecordID, RollID) VALUES(1, 1, 2);
INSERT INTO musiciantorecord (MusicianID, RecordID, RollID) VALUES(1, 1, 3);
INSERT INTO musiciantorecord (MusicianID, RecordID, RollID) VALUES(1, 1, 4);
INSERT INTO musiciantorecord (MusicianID, RecordID, RollID) VALUES(1, 1, 6);
INSERT INTO musiciantorecord (MusicianID, RecordID, RollID) VALUES(2, 1, 8);
INSERT INTO musiciantorecord (MusicianID, RecordID, RollID) VALUES(3, 1, 7);
INSERT INTO musiciantorecord (MusicianID, RecordID, RollID) VALUES(4, 1, 5);
INSERT INTO musiciantorecord (MusicianID, RecordID, RollID) VALUES(5, 1, 5);

INSERT INTO musiciantoband (MusicianID, bandID, RollID, yearStart, yearEnd, known) VALUES(1, 1, 2, 1992, -1, 1);
INSERT INTO musiciantoband (MusicianID, bandID, RollID, yearStart, yearEnd, known) VALUES(1, 1, 3, 1992, -1, 1);
INSERT INTO musiciantoband (MusicianID, bandID, RollID, yearStart, yearEnd, known) VALUES(1, 1, 4, 1992, -1, 1);
INSERT INTO musiciantoband (MusicianID, bandID, RollID, yearStart, yearEnd, known) VALUES(1, 1, 6, 1992, -1, 1);
INSERT INTO musiciantoband (MusicianID, bandID, RollID, yearStart, yearEnd, known) VALUES(2, 1, 8, 1992, -1, 1);
INSERT INTO musiciantoband (MusicianID, bandID, RollID, yearStart, yearEnd, known) VALUES(3, 1, 7, 1992, 1998, 0);
INSERT INTO musiciantoband (MusicianID, bandID, RollID, yearStart, yearEnd, known) VALUES(4, 1, 5, 1992, -1, 1);