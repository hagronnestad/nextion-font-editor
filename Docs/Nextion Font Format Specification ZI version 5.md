# ZI font format version 5 specification
❗ This is an unfinished reversed engineered specification the TJC USART HMI ZI font format (version 5). ❗
Use at own risk.

## General information

The Nextion Font Format is a proprietary font format used by the Nextion/USART Editor HMI software. The editor has a built in "Font Generator"-tool which converts standard fonts into .zi files that is compatible with the Nextion and TJC HMI displays.

| Information             |                       |
|-------------------------|-----------------------|
| **Software**            | Nextion Editor V0.55  |
| **File extension**      | .zi                   |
| **File format version** | ZI version 5          |
| **Example file**        | Consolas_16_ascii.zi  |

## Code pages / character encoding reference

*to be verified* Likely the same as v3.

| Code page / encoding  | Value                 | Number of characters  |
|-----------------------|-----------------------|-----------------------|
| ASCII                 | 0x01                  | 95                    |
| GB2312                | 0x02                  | 8273                  |
| ISO-8859-1            | 0x03                  | 224                   |
| ISO-8859-2            | 0x04                  | 224                   |
| ISO-8859-3            | 0x05                  | 224                   |
| ISO-8859-4            | 0x06                  | 224                   |
| ISO-8859-5            | 0x07                  | 224                   |
| ISO-8859-6            | 0x08                  | 224                   |
| ISO-8859-7            | 0x09                  | 224                   |
| BIG5                  | 0x1001                | 14225                 |
| ISO-8859-8            | 0x0A                  | 224                   |
| ISO-8859-9            | 0x0B                  | 224                   |
| ISO-8859-13           | 0x0C                  | 224                   |
| ISO-8859-15           | 0x0D                  | 224                   |
| ISO-8859-11           | 0x0E                  | 224                   |
| KS_C_5601-1987        | 0x0F                  | 3855                  |
| WINDOWS-1255          | 0x11                  | 224                   |
| WINDOWS-1256          | 0x12                  | 224                   |
| WINDOWS-1257          | 0x13                  | 224                   |
| WINDOWS-1258          | 0x14                  | 224                   |
| WINDOWS-874           | 0x15                  | 224                   |
| KOI8-R                | 0x16                  | 224                   |


## File format structure

### Fixed header

**Length:** 0x2C (44)

```
0x00000000: 04 FF 00 0A 01 00 14 28 00 00 20 7E 5F 00 00 00
0x00000010: 03 0E 0E 00 2A 25 00 00 00 00 00 00
```

| Offset     | Length | Data                                             | Type   | Value               | Description                                                                           |
|------------|-------:|--------------------------------------------------|--------|--------------------:|---------------------------------------------------------------------------------------|
| 0x00000000 | 3      | `04 FF 00` *                                  | byte[] |                     | File signature / magic numbers                                                        |
| 0x00000003 | 1      | `0A`                                  | byte | 10                    | Orientation (10 = 0° Vertical, 11 = 90° Horizontal, 12 = 180° Vertical, 13 = 270° Hprizontal)       |
| 0x00000004 | 2      | `01 00`                                          | uint16 | 1                   | Encoding & State                                                                      |
| 0x00000006 | 1      | `00`                                             | byte   | 0                   | Character width = 0 for variable width fonts                                          |
| 0x00000007 | 1      | `28`                                             | byte   | 40                  | Character height                                                                      |
| 0x00000008 | 1      | `00`                                             | byte   | 0                   | Code page multibyte - first byte start                                                |
| 0x00000009 | 1      | `00`                                             | byte   | 0                   | Code page multibyte - first byte end                                                  |
| 0x0000000A | 1      | `20`                                             | byte   | 32, ' ' (ASCII)     | Code page start / multibyte second byte start                                         |
| 0x0000000B | 1      | `7E`                                             | byte   | 126, '~' (ASCII)    | Code page end / multibyte second byte end                                             |
| 0x0000000C | 4      | `5F 00 00 00`                                    | uint32 | 95                  | Number of characters in file                                                          |
| 0x00000010 | 1      | `05`                                             | byte   | 5                   | Font File Version                                                                     |
| 0x00000011 | 1      | `11`                                             | byte   | 17                  | Length of font name                                                                   |
| 0x00000012 | 2      | `00 00`                                          | uint16 | 0                   | ~~~~Also length of font name? Always the same value as 0x11 ~~                        |
| 0x00000014 | 4      | `2A 25 00 00`                                    | uint32 | 9514                | Total length of font name and character data                                          |
| 0x00000018 | 4      | `2C 00 00 00`                                    | uint32 | 0                   | Start of Data Address (= Font Name location)                                          |
| 0x0000001C | 1      | `00`                                             | byte   | 0                   | Character width = 0 for variable width fonts                                          |
| 0x0000001D | 1      | `28`                                             | byte   | 40                  | Character height                                                                      |
| 0x0000001E | 1      | `00`                                             | byte   | 0                   | Code page multibyte - first byte start                                                |
| 0x00000021 | 1      | `00`                                             | byte   | 0                   | Reserved                                                                              |
| 0x00000022 | 2      | `00 00`                                          | uint16 | 0                   | Reserved                                                                              |
| 0x00000024 | 4      | `00 00 00 00`                                    | uint32 | 0                   | Reserved                                                                              |
| 0x00000028 | 4      | `00 00 00 00`                                    | uint32 | 0                   | Reserved                                                                              |
| 0x0000002C | #      | ``                                    | byte[] | 0                   | Fontname                                                                              |
| 0x0000003D | 2      | `20 00`                                          | uint16 | space               | Start of Character Map **(see below)**                                                |

> **\* The file signature/magic bytes for a .zi file containing the `BIG5` code page is different from all other files. For `BIG5` the magic bytes are `04 7E 22 0A`. Which means that the second and third byte might be variable and have some meaning beyond being magic numbers.**

### Font name
Variable length font name. In this case the font name is `0x11 (17)` bytes long as seen in offset `0x00000011`. The start of the fontname is indicated by offset `0x00000018` and should be `0x0000002C` since the header size is fixed.

```
0x00000010: 03 0E 0E 00 2A 25 00 00 00 00 00 00 41 72 69 61
0x00000020: 6C 5F 34 30 5F 61 73 63 69 69 00 00 00 00 00 00
```

| Offset     | Length | Data                                             | Type   | Value               | Description                                                      |
|------------|-------:|--------------------------------------------------|--------|--------------------:|------------------------------------------------------------------|
| 0x0000001C | 14     | `41 72 69 61 6C 5F 34 30 5F 61 73 63 69 69`      | string | Arial_40_ascii      | Font name                                                        |

### Character Map

Next the file contains a fixed size lookup table for each character with pointers to the actual location of the pixel data.
The Character map length is 10 * **&lt;Number of characters in file&gt;** as found in offset `0x0000000C`.
Each character entry is 10 bytes long:

| Offset     | Length | Data                                             | Type   | Value               | Description                                                      |
|------------|-------:|--------------------------------------------------|--------|--------------------:|------------------------------------------------------------------|
| 0x0000003D | 2      | `20 00`                                  | uint16 | space               | Character of the Code page                                                         |
| 0x0000003F | 1      | `08`                                     | byte | 8                   | Character width                                |
| 0x00000040 | 1      | `00`                                     | byte   | 0                   | Character length?          |
| 0x00000041 | 1      | `00`                                     | byte   | 0                   | Character rotation?          |
| 0x00000042 | 3      | `B6 03 00`                               | byte[3] | 950                 | Start byte of the character data, as an offset from the start of the charactermap (in this case 0x3D)      |
| 0x00000045 | 2      | `06 00`                                  | uint16 | 6                   | Length of the character data           |
|  |       |                                   |  |                    |            |
| 0x00000047 |  #      | `21 00`                                  | uint16 | !                   | **next character in lookup table**       |


### Character Data

The rest of the file contains the binary representation of each character with variable lengths.
The actual format of the characters is yet to be determined.

### Character data example for a 16 pixels tall exclamation mark (`!`) character

To be investigated.