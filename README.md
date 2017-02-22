# tweet-downloader-sharp

이 프로그램은 트위터에서 키워드로 검색하여 결과 트윗에 첨부된 이미지를 다운로드 받습니다. [tweet-downloader-py](https://github.com/overworks/tweet-downloader-py)를 C#으로 포팅한 것입니다. 

## 사용방법

1. [.NET Framework 3.5 이상을 설치합니다.](https://www.microsoft.com/net/download/framework). 윈도7 이상을 사용중이라면 이미 설치되어 있을 것입니다.
2. 명령 프롬프트를 열고 다음 명령어로 실행시킵니다. `tweet-downloader-sharp.exe [OPTION] [ARGUMENT] 검색어`
3. 기존에 액세스 토큰을 받은 적이 없다면 브라우저 창이 열리면서 인증을 요구합니다. 여기서 앱 인증을 선택후 7자리의 PIN 코드를 입력합니다.
4. 해당 디렉토리에 파일이 다운로드됩니다.

## 옵션 및 인수

| 옵션               | 인수          | 설명                                                    |
| ------------------ | ------------- | ------------------------------------------------------- |
| -s, --silence      |               | 화면에 다운로드 메시지를 출력하지 않습니다.             |
| -rt, --retweet     | [NUMBER]      | [NUMBER]회 이상 리트윗된 트윗 미디어만 다운로드합니다.  |
| -d, --directory    | [PATH]        | 다운로드한 미디어를 [PATH] 디렉토리 안에 저장합니다.    |
| -sn, --screen_name | [SCREEN_NAME] | 트위터 유저 [SCREEN_NAME]의 트윗 내에서 검색합니다.     |

## 예시

ありふみ로 검색해서 첨부된 미디어를 메시지를 표시하지 않고 현재 디렉토리에 저장
```
tweet-downloader-sharp.exe --silence ありふみ
```

150회 이상 리트윗된 ありふみ 트윗에 첨부된 미디어를 arifumi 디렉토리에 저장
```
tweet-downloader-sharp.exe --retweet 150 --directory arifumi ありふみ
```

## 기타

- 트윗에 여러개의 동영상이 첨부되어 있는 경우에는 섬네일만 다운로드 받습니다.
- 그 외에 많은 문제가 존재할 수 있습니다. 버그나 문의사항에 관해서는 트위터 [@lostland](https://twitter.com/lostland)로 연락바랍니다.
- 신데렐라 걸즈 최고 커플링은 아리후미이며, 이것은 과학으로도 증명할 수 있습니다.