namespace AdventOfCode20.PasswordPhilosophy

type Password = string

type Policy = { Letter: char; MinCount: int; MaxCount: int }

type PasswordLog = { Policy: Policy; Password: Password }

type PasswordReport = Logs of PasswordLog list
