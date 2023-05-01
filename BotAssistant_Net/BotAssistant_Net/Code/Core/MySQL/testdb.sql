-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 01 Maj 2023, 18:27
-- Wersja serwera: 10.4.27-MariaDB
-- Wersja PHP: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `testdb`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `questions`
--

CREATE TABLE `questions` (
  `questionID` int(11) NOT NULL,
  `usersID` int(11) NOT NULL,
  `question` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `questions`
--

INSERT INTO `questions` (`questionID`, `usersID`, `question`) VALUES
(20005, 12, 'ping'),
(20006, 12, 'Test 1'),
(20007, 12, 'Test 3'),
(20008, 12, 'Tom'),
(20009, 12, 'Info');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `userteacher`
--

CREATE TABLE `userteacher` (
  `usersID` int(11) NOT NULL,
  `username` text NOT NULL,
  `firstMarkDate` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Zrzut danych tabeli `userteacher`
--

INSERT INTO `userteacher` (`usersID`, `username`, `firstMarkDate`) VALUES
(12, '@Tomsztyl#9278', '5/1/2023 4:01:00 PM'),
(13, '@Tomsztyl#9278', '5/1/2023 4:37:32 PM'),
(14, '@Tomsztyl#9278', '5/1/2023 4:37:44 PM'),
(15, '@Tomsztyl#9278', '5/1/2023 4:47:48 PM'),
(16, '@Tomsztyl#9278', '5/1/2023 4:48:40 PM'),
(17, '@Tomsztyl#9278', '5/1/2023 4:50:03 PM');

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`questionID`),
  ADD KEY `usersID` (`usersID`);

--
-- Indeksy dla tabeli `userteacher`
--
ALTER TABLE `userteacher`
  ADD PRIMARY KEY (`usersID`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `questions`
--
ALTER TABLE `questions`
  MODIFY `questionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20010;

--
-- AUTO_INCREMENT dla tabeli `userteacher`
--
ALTER TABLE `userteacher`
  MODIFY `usersID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `questions`
--
ALTER TABLE `questions`
  ADD CONSTRAINT `questions_ibfk_1` FOREIGN KEY (`usersID`) REFERENCES `userteacher` (`usersID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
