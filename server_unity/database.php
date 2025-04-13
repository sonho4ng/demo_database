CREATE TABLE Players (
    id INT NOT NULL AUTO_INCREMENT,    
    username VARCHAR(50) NOT NULL,      
    hash VARCHAR(100) NOT NULL,          
    salt VARCHAR(50) NOT NULL,         
    score INT NOT NULL DEFAULT 0,       
    PRIMARY KEY (id)                    
);
