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


-- CREATE TABLE contractors 
-- ( 
--   id INT NOT NULL AUTO_INCREMENT, 
--   title VARCHAR(255) NOT NULL,
--   creatorId VARCHAR(255) NOT NULL, 
--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );


-- CREATE TABLE contractorjobs
-- (
--   id INT NOT NULL AUTO_INCREMENT, 
--   jobId INT,
--   contractorId INT,
--   creatorId VARCHAR(255),
--   PRIMARY KEY (id),

--    FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (jobId)
--    REFERENCES jobs (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (contractorId)
--    REFERENCES contractors (id)
--    ON DELETE CASCADE

-- )

-- DROP TABLE whiteboards;
-- CREATE TABLE whiteboards
-- (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   public TINYINT(1),

--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--     REFERENCES profiles (id)
--     ON DELETE CASCADE

-- );
DROP TABLE stickynotes;
CREATE TABLE stickynotes
(
  id INT NOT NULL AUTO_INCREMENT,
  memberId VARCHAR(255) NOT NULL,
  whiteboardId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,

    PRIMARY KEY (id),

    FOREIGN KEY (creatorId)
    REFERENCES profiles (id)
    ON DELETE CASCADE,

    FOREIGN KEY (memberId)
    REFERENCES profiles (id)
    ON DELETE CASCADE,

    FOREIGN KEY (whiteboardId)
    REFERENCES whiteboards (id)
    ON DELETE CASCADE

)
