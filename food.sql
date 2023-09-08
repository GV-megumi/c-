
/* SOURCE C:/Users/gv201/Desktop/food.sql*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for food_i
-- ----------------------------
DROP TABLE IF EXISTS `food_i`;
CREATE TABLE `food_i` (
  `name` char(10) NOT NULL,
  `P_C` varchar(20) DEFAULT NULL,
  `I_N_D` varchar(20) DEFAULT NULL,
  `S_C` varchar(20) DEFAULT NULL,
  `NUTR` char(40) DEFAULT NULL,

  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;





-- ----------------------------
-- Records of food_i
-- ----------------------------
BEGIN;
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('番茄','蔬菜','无','常温干燥处保存','富含维生素c等');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('鸡蛋','蛋制品','无','冷藏干燥处保存','富含蛋白质等');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('大葱','蔬菜','无','冷藏较湿润处保存','富含维生素c等');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('青椒','蔬菜','无','冷藏干燥处保存','富含维生素c等');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('大蒜','蔬菜','无','常温干燥处保存','富含维生素A等');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('黄瓜','蔬菜','无','冷藏干燥处保存','富含维生素c等');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('盐','调味品','无','常温干燥处保存','富含人体所需微量元素');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('味精','调味品','无','常温干燥处保存','富含人体所需微量元素');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('鸡精','调味品','无','常温干燥处保存','富含人体所需微量元素');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('白糖','调味品','无','常温干燥处保存','富含人体所需微量元素');
INSERT INTO `food_i` (`name`,`P_C`,`I_N_D`,`S_C`,`NUTR`) 
VALUES ('食用油','调味品','无','常温干燥处保存','富含人体所需微量元素');
COMMIT;









-- ----------------------------
-- Table structure for SUP
-- ----------------------------
DROP TABLE IF EXISTS `SUP`;
CREATE TABLE `SUP` (
  `M_F` char(20) NOT NULL,
  `address` varchar(40) DEFAULT NULL,
  `poo` varchar(40) DEFAULT NULL,
  `FPLN` varchar(40) DEFAULT NULL,
  `PSN` varchar(40) DEFAULT NULL,
  

  PRIMARY KEY (`M_F`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;



-- ----------------------------
-- Records of SUP
-- ----------------------------
BEGIN;
INSERT INTO `SUP` (`M_F`,`address`,`poo`,`FPLN`,`PSN`) 
VALUES ('甲','甲市','甲农业基地','20230901','01');
INSERT INTO `SUP` (`M_F`,`address`,`poo`,`FPLN`,`PSN`) 
VALUES ('乙','乙市','乙农业基地','20230902','02');
INSERT INTO `SUP` (`M_F`,`address`,`poo`,`FPLN`,`PSN`) 
VALUES ('丙','丙市','丙农业基地','20230903','03');
INSERT INTO `SUP` (`M_F`,`address`,`poo`,`FPLN`,`PSN`) 
VALUES ('丁','丁市','丁农业基地','20230904','04');
INSERT INTO `SUP` (`M_F`,`address`,`poo`,`FPLN`,`PSN`) 
VALUES ('戊','戊市','戊农业基地','20230905','05');
INSERT INTO `SUP` (`M_F`,`address`,`poo`,`FPLN`,`PSN`) 
VALUES ('己','己市','己农业基地','20230906','06');
COMMIT;






-- ----------------------------
-- Table structure for SOG
-- ----------------------------
DROP TABLE IF EXISTS `SOG`;
CREATE TABLE `SOG` (
  `SOG_ID` INT NOT NULL AUTO_INCREMENT,
  `NAME` char(10) NOT NULL,
  `M_F` char(20) NOT NULL,

  UNIQUE KEY (`SOG_ID`),
  PRIMARY KEY (`NAME`,`M_F`),
  FOREIGN KEY (`NAME`) REFERENCES food_i(`name`),
  FOREIGN KEY (`M_F`) REFERENCES SUP(`M_F`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;




-- ----------------------------
-- Records of SOG
-- ----------------------------
BEGIN;
INSERT INTO `SOG` (`NAME`,`M_F`) 
VALUES ('番茄','甲');

INSERT INTO `SOG` (`NAME`,`M_F`) 
VALUES ('番茄','丁');
INSERT INTO `SOG` (`NAME`,`M_F`) 
VALUES ('鸡蛋','甲');
INSERT INTO `SOG` (`NAME`,`M_F`) 
VALUES ('青椒','丁');
INSERT INTO `SOG` (`NAME`,`M_F`) 
VALUES ('番茄','乙');
INSERT INTO `SOG` (`NAME`,`M_F`) 
VALUES ('黄瓜','戊');
INSERT INTO `SOG` (`NAME`,`M_F`) 
VALUES ('白糖','甲');
COMMIT;





-- ----------------------------
-- Table structure for inv
-- ----------------------------
DROP TABLE IF EXISTS `inv`;
CREATE TABLE `inv` (
  `sog_id` int NOT NULL,
  `date` varchar(10) NOT null,
  `nc` varchar(20) DEFAULT NULL,
  `sl` varchar(20) DEFAULT NULL,
  `number` int DEFAULT NULL,

  PRIMARY KEY (`sog_id`,`date`),
  FOREIGN KEY (`sog_id`) REFERENCES SOG(`SOG_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;








-- ----------------------------
-- Records of inv
-- ----------------------------
BEGIN;
INSERT INTO `inv` (  `sog_id`,`date`,`nc`,`sl`,`number`) 
VALUES ( 01,'2023.07.02','100g','60天',10);
INSERT INTO `inv` (  `sog_id`,`date`,`nc`,`sl`,`number`) 
VALUES ( 03,'2023.07.02','100g','60天',10);
INSERT INTO `inv` (  `sog_id`,`date`,`nc`,`sl`,`number`) 
VALUES ( 02,'2023.07.02','100g','60天',10);
INSERT INTO `inv` (  `sog_id`,`date`,`nc`,`sl`,`number`) 
VALUES ( 04,'2023.07.02','100g','60天',10);
INSERT INTO `inv` (  `sog_id`,`date`,`nc`,`sl`,`number`) 
VALUES ( 05,'2023.07.02','100g','60天',10);
INSERT INTO `inv` (  `sog_id`,`date`,`nc`,`sl`,`number`) 
VALUES ( 01,'2023.08.02','100g','60天',10);
INSERT INTO `inv` (  `sog_id`,`date`,`nc`,`sl`,`number`) 
VALUES ( 07,'2023.08.02','100g','60天',10);
COMMIT;







-- ----------------------------
-- Table structure for rmm
-- ----------------------------
DROP TABLE IF EXISTS `rmm`;
CREATE TABLE `rmm` (
  `name` varchar(20) NOT NULL,
  `M_I` varchar(40) DEFAULT null,
  `S_S` varchar(40) DEFAULT NULL,
  `R_S` varchar(100) DEFAULT NULL,
  `R_C` varchar(10) DEFAULT NULL,
  `M_V` varchar(10) DEFAULT NULL,


  PRIMARY KEY (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;







-- ----------------------------
-- Records of rmm
-- ----------------------------
BEGIN;
INSERT INTO `rmm` (  `name` ,`M_I`,`S_S` ,`R_S` ,`R_C`,`M_V`) 
VALUES ('番茄炒蛋','番茄,鸡蛋,大葱','食用油,盐,味精,鸡精,白糖','一锅炖','家常菜','素');
INSERT INTO `rmm` (  `name` ,`M_I`,`S_S` ,`R_S` ,`R_C`,`M_V`) 
VALUES ('青椒炒蛋','青椒,鸡蛋,大葱,大蒜','食用油,盐,味精,鸡精,白糖','一锅炖','家常菜','素');
INSERT INTO `rmm` (  `name` ,`M_I`,`S_S` ,`R_S` ,`R_C`,`M_V`) 
VALUES ('黄瓜炒蛋','黄瓜,鸡蛋,大葱','食用油,盐,味精,鸡精,白糖','一锅炖','家常菜','素');
INSERT INTO `rmm` (  `name` ,`M_I`,`S_S` ,`R_S` ,`R_C`,`M_V`) 
VALUES ('火山飘雪','番茄','白糖','凉拌','家常菜','素');
COMMIT;
















SET FOREIGN_KEY_CHECKS = 1;
