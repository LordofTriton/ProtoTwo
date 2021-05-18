-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: May 01, 2021 at 08:16 PM
-- Server version: 8.0.21
-- PHP Version: 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `vmsdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `visitordb`
--

DROP TABLE IF EXISTS `visitordb`;
CREATE TABLE IF NOT EXISTS `visitordb` (
  `sn` int UNSIGNED NOT NULL AUTO_INCREMENT,
  `firstname` varchar(30) NOT NULL,
  `lastname` varchar(30) NOT NULL,
  `phone` varchar(30) NOT NULL,
  `check_in` varchar(30) NOT NULL,
  `check_out` varchar(30) NOT NULL,
  `purpose` varchar(30) NOT NULL,
  `meeting` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `tag` varchar(30) NOT NULL,
  `reg_date` date NOT NULL,
  PRIMARY KEY (`sn`)
) ENGINE=MyISAM AUTO_INCREMENT=25 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `visitordb`
--

INSERT INTO `visitordb` (`sn`, `firstname`, `lastname`, `phone`, `check_in`, `check_out`, `purpose`, `meeting`, `tag`, `reg_date`) VALUES
(1, 'Joshua', 'Agboola', '07065194708', '08:09', '02:26', 'Internship', '', '33490', '2021-04-27'),
(2, 'Idowu', 'Ajala', '08116784565', '07:44', '01:00', 'Internship', '', '09891', '2021-04-27'),
(3, 'Nathan', 'Daniels', '09077568776', '08:01', '', 'Business', '', '46467', '2021-04-27'),
(4, 'Johnny', 'Depp', '07065194708', '09:14', '02:30', 'Business', '', '77564', '2021-04-27'),
(5, 'Joe', 'West', '08193769813', '11:39', '01:20', 'Inquiry', '', '88893', '2021-04-27'),
(6, 'Dominic', 'Toretto', '07076745837', '15:13', '02:26', 'Partnership', '', '10675', '2021-04-27'),
(7, 'Sherlock', 'Holmes', '08083497765', '08:55', '01:21', 'Contraction', 'Nicholas FC. Enyika', '90389', '2021-04-27'),
(8, 'Robert', 'Downey', '+1 6579856545', '08:55', '02:27', 'Contraction', 'Joshua Agboola', '78951', '2021-04-27'),
(9, 'Dwayne', 'Johnson', '+1 8775747878', '14:56', '02:29', 'Business', 'Blessing Sado', '99657', '2021-04-26'),
(10, 'Robert', 'Lewandowski', '+1 5567835454', '08:39', '02:05', 'Partnership', 'ICT Infrastructure', '13458', '2021-04-26'),
(14, 'Sergio', 'Ramos', '08116548897', '12:33', '01:21', 'Business', 'Admin', '19471', '2021-04-27'),
(12, 'Bill', 'Gates', '+1 8764557637', '16:09', '02:14', 'Negotiations', 'Admin', '47116', '2021-04-27'),
(13, 'Lemar', 'Hoskins', '+1 8775747878', '10:28', '02:27', 'Partnership', 'ICT Infrastructure', '25838', '2021-04-27'),
(15, 'Harry', 'Potter', 'None', '04:06', '09:10', 'Investigation', 'Everybody', '44346', '2021-04-27'),
(16, 'Robert', 'Lewandowski', '+15567835454', '04:47', '11:41', 'Business', 'ICT Infrastructure', '63445', '0000-00-00'),
(17, 'Aliko', 'Dangote', '07065487441', '10:33', '12:56', 'Partnership', 'Mudashir B. Lawal', '55987', '0000-00-00'),
(18, 'Donald', 'Trump', '+1 8775747878', '10:33', '12:56', '', 'Mudashir B. Lawal', '78633', '0000-00-00'),
(19, 'Muhammadu', 'Buhari', '08117704576', '10:40', '08:51', '', 'Mudashir B. Lawal', '45694', '0000-00-00'),
(20, 'Van', 'Helsing', '+1 5674783', '12:45', '', 'Extermination', 'Admin', '44020', '0000-00-00'),
(21, 'Alexander', 'Pope', '08035567845', '12:48', '', 'Business', 'Marketing', '64938', '0000-00-00'),
(22, 'Henny', 'Penny', 'None', '12:51', '', 'Complaint', 'Human Resources', '44893', '0000-00-00'),
(23, 'Random', 'Person', 'Random Number', '01:32', '', 'Loitering', 'Everyone', '97057', '0000-00-00'),
(24, 'Aliko', 'Dangote', '07065487441', '08:52', '', 'Partnership', 'Mudashir B. Lawal', '55987', '0000-00-00');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
