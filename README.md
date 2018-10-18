# Nextion Font Editor


## Nextion Font Format Specification
This is a reverse engineered specification of the Nextion font format. Use at own risk.

|Information            |                       |
|-----------------------|-----------------------|
| **Example file**      | Arial_40_ascii.zi     |
| **Software**          | Nextion Editor        |
| **File extension**    | .zi                   |


### Fixed header

**Length:** 0x1C (28)

```
00000000: 04 FF 00 0A 01 00 14 28 00 00 20 7E 5F 00 00 00
00000010: 03 0E 0E 00 2A 25 00 00 00 00 00 00
```

| Offset     | Length | Data                                             | Type   | Value               | Description                                                      |
|------------|-------:|--------------------------------------------------|--------|--------------------:|------------------------------------------------------------------|
| 0x00000000 | 6      | `04 FF 00 0A 01 00`                              |        |                     | Fixed?                                                           |
| 0x00000006 | 1      | `14`                                             | byte   | 20                  | Character width                                                  |
| 0x00000007 | 1      | `28`                                             | byte   | 40                  | Character height                                                 |
| 0x0000000C | 4      | `5F 00 00 00`                                    | uint32 | 95                  | Number of characters in file                                     |
| 0x00000011 | 1      | `0E`                                             | byte   | 14                  | Length of font name                                              |
| 0x00000012 | 1      | `0E`                                             | byte   |                     | Also length of font name? Seems to always be the same as 0x11    |
| 0x00000014 | 4      | `2A 25 00 00`                                    | uint32 | 9514                | Total length of font name and character data                     |
| 0x00000018 | 4      | `00 00 00 00`                                    |        |                     | Unknown, font name comes after these                             |

### Font name
Variable length font name. In this case the font name is `0x0E (14)` bytes long as seen in offset `0x00000011`.

```
00000010: 03 0E 0E 00 2A 25 00 00 00 00 00 00 41 72 69 61
00000020: 6C 5F 34 30 5F 61 73 63 69 69 00 00 00 00 00 00
```

| Offset     | Length | Data                                             | Type   | Value               | Description                                                      |
|------------|-------:|--------------------------------------------------|--------|--------------------:|------------------------------------------------------------------|
| 0x0000001C | 14     | `41 72 69 61 6C 5F 34 30 5F 61 73 63 69 69`      | string | Arial_40_ascii      | Font name                                                        |
