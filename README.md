# Nextion Font Suite

A collection of tools to work with Nextion Fonts.

| Branch | Status |
|--------|--------|
| master | [![Build status](https://ci.appveyor.com/api/projects/status/kmi5iikvsod53c4p?svg=true)](https://ci.appveyor.com/project/hagronnestad/nextion-font-editor) |


## Roadmap

#### Prioritized tasks
- [ ] Reverse engineering of the new Nextion font format supporting anti-aliasing and variable width fonts (TJC USART HMI version 0.55)
- [ ] Support for anti-aliased and variable width fonts for (TJC USART HMI version 0.55)

#### Less prioritized tasks
- [x] Reverse engineering of the Nextion font format version 3 for Nextion Editor V0.53
- [x] Font Viewer
- [x] Font Editor
- [x] Font generator
- [ ] Support for most code pages supported by Nextion Editor
  - [x] ASCII
  - [x] ISO-8859-1
  - [ ] Others

> Sample screenshot of the "Font Editor"-tool previewing the `$` character from the `Arial_40_ascii.zi` file.

![](Screenshots/02-thumb.png)

> Sample screenshot of the "Font Preview"-tool previewing the `Arial_40_ascii.zi` file.

![](Screenshots/01-thumb.png)

## Nextion .ZI Font Format Specification

### ZI version 3 specification

A mostly complete reverse engineered specification of the Nextion font format (ZI version 3), can be found here:

[ZI version 3 specification](Docs/Nextion%20Font%20Format%20Specification%20ZI%20version%203.md)

### ZI version 5 specification

I recently started reverse engineering the new ZI file format version 5 used in TJCs USART HMI editor version `0.55`. ZI version 5 supports variable width fonts and antialiasing. This specification is very much a work in progress. Feel free to contribute.

[ZI version 5 specification (❗ Work in progress ❗)](Docs/Nextion%20Font%20Format%20Specification%20ZI%20version%205.md)

### TJCs USART HMI English Patch
If you want to try the new font features and have a TJC panel, you can use this patch to run USART HMI in english.

[TJCs USART HMI English Patch](https://github.com/hagronnestad/tjc-usart-hmi-english-patch)