INSERT INTO bands (bandid, name, cityid, createdAt, UpdatedAt)
VALUES (0, 'UNKNOWN', 0, now(), now());
INSERT INTO cities (cityid, name, country, createdAt, UpdatedAt)
VALUES (0, 'UNKNOWN', 'UNKNOWN', now(), now());
INSERT INTO records (RecordId, Name, BandId, Year, Domain, LabelId, Zeitgeist, Certification, CreatedAt, UpdatedAt)
VALUES (0, 'UNKNOWN', 0, 0, 0, 0, 0, '', now(), now());
INSERT INTO labels (LabelId, Name, CityId, CreatedAt, UpdatedAt)
VALUES (0, 'UNKNOWN', 0, now(), now());
UPDATE bands SET bandid=0 WHERE name = 'UNKNOWN';
UPDATE records SET recordsid=0 WHERE name = 'UNKNOWN';
UPDATE cities SET cityid=0 WHERE name = 'UNKNOWN';
UPDATE labels SET labelid=0 WHERE name = 'UNKNOWN';