$usings, $rest = (Get-Content *.cs).Where({ $_ -match '^\s*using\s' }, 'Split')

# Encoding note: Creates a BOM-less UTF-8 file in PowerShell [Core] 6+,
#                and an ANSI file in Windows PowerShell. Use -Encoding as needed.
Set-Content bigBadFile.txt -Value (@($usings | Select-Object -Unique) + $rest)