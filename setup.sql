USE jobscontractors;
-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );

-- CREATE TABLE jobs
-- ( 
--   id INT NOT NULL AUTO_INCREMENT,
--   title VARCHAR(255) NOT NULL,
--   description VARCHAR(255),
--   price DECIMAL(6, 2) NOT NULL,
--   creatorId VARCHAR(255) NOT NULL, 
--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );


-- CREATE TABLE wishlists 
-- ( 
--   id INT NOT NULL AUTO_INCREMENT, 
--   title VARCHAR(255) NOT NULL,
--   creatorId VARCHAR(255) NOT NULL, 
--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );


-- CREATE TABLE wishlistjobs
-- (
--   id INT NOT NULL AUTO_INCREMENT, 
--   jobId INT,
--   wishlistId INT,
--   creatorId VARCHAR(255),
--   PRIMARY KEY (id),

--    FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (jobId)
--    REFERENCES jobs (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (wishlistId)
--    REFERENCES wishlists (id)
--    ON DELETE CASCADE

-- )