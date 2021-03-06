## 김효건 LyricsProject
 ----------------------------------------------------------------------------------------------
# 프로젝트 소개 <br>
![player](./Presto.SWCamp.Lyrics/image/wd.PNG)
### UI 소개
#### Material Design Package를 활용하여 깔끔하고 세련 된 디자인
 - '+' 버튼 : 전체 가사 출력 모드 시 글자를 확대한다.
 - '-' 버튼 : 전체 가사 출력 모드 시 글자를 축소한다.
 - 종료 버튼 : 프로그램을 종료한다.
 - 'i' 버튼 : 현재 재생 되는 곡의 정보를 출력한다.
 - '1' 버튼 : 1줄 가사 출력 모드로 전환한다.
 - '≡' 버튼 : 전체 가사 출력 모드로 전환한다.
 - 일시정지 버튼 : 곡의 재생을 일시정지/재생 한다.
 - '<' 버튼 : 이전 노래를 재생한다.
 - '>' 버튼 : 다음 노래를 재생한다.
 - 가운데 원 안의 사진의 해당 곡의 앨범 사진으로 변경 된다.
 - 전체 가사 출력 모드 시 현재 재생 중인 가사만 색상이 변한다.
 - 전체 가사 출력 모드 시 원하는 가사를 클릭하면 재생이 그 시점으로 이동한다.
 - Player에서 재생 위치를 변경하면 해당 가사 창에서도 해당 위치로 이동한다.

### 기능 소개
   * 플레이어 싱행 후 노래를 재생 시키면 Default로 싱크에 맞게 한 줄 가사 출력
   * part가 있는 노래의 경우 텍스트를 재구성 하여 모든 가사에 파트 지정해 출력<br>
   ![player](./Presto.SWCamp.Lyrics/image/part.PNG)
   * 외국 노래의 경우 같은 시간 대에 번역 등 여러줄 한번에 출력 (2줄,3줄,4줄, ... 모두 가능)<br>
   ![player](./Presto.SWCamp.Lyrics/image/3.PNG)
   ![player](./Presto.SWCamp.Lyrics/image/32.PNG)
   * 한줄 가사 모드와 전체 가사 모두 자유롭게 선택 가능<br>
   ![player](./Presto.SWCamp.Lyrics/image/1줄.PNG)
   ![player](./Presto.SWCamp.Lyrics/image/전체.PNG)
   * 현재 재생 되는 곡에 맞게 album 이미지가 변경 됨<br>
   ![player](./Presto.SWCamp.Lyrics/image/d.PNG)
   ![player](./Presto.SWCamp.Lyrics/image/w.PNG)
   ![player](./Presto.SWCamp.Lyrics/image/여행.PNG)
   * 전체 가사 출력 모드 시 가사를 클릭하면 해당 가사로 재생 시점이 이동한다.
   * Player에서 재생 시점을 변경하면 가사 위치도 변경 된다.
   * info 버튼을 누르면 현재 재생되는 곡의 정보를 MessageBox 형태로 출력한다.<br>
   ![player](./Presto.SWCamp.Lyrics/image/info.PNG)
   
 ----------------------------------------------------------------------------------------------
# 일별 진척 사항
  ### 2018-11-24
 * 싱크에 맞춰서 가사 출력하기
 * 배열 형태로 가사를 저장한 것을 SortedList로 변경
   - (메모리 낭비 줄이기 위해 가변적인 자료구조 사용)
 * 마지막 가사 출력 후 인덱스 초과 오류 방지 수정
 * Twice Dance The Night Away 형식 실행 가능하도록 수정
   - (plit 했을 때 3개인 경우와 2개인 경우로 나누어서 이전의 파트 없는 노래도 문제없이 재생 가능)
 * 파싱한 데이터를 저장하는 변수를 SortedList<TimeSpan,string>에서 List<Tuple<TimeSpan,string>>으로 변경
   - (일본곡 재생을 위해서 동일한 키 값 사용을 위해)
-------------------------------------------------------------------------------------
   ### 2018-11-26
  * 브랜치로 빼서 가사 출력을 전체가사를 출력하고 재생되는 가사의 배경만 색을 바꾸는 식으로 수정
    - 스크롤 추가 
    - 마지막 가사 오류
  * 마지막 가사 오류 수정
    - (인덱스 수정)
    - ~~~
      else if (cur >= splitData[splitData.Count() - 1].Item1) //마지막 가사 출력 (인덱스 오류 방지)
                  {
                      //textLyrics.Text = splitData[splitData.Count() - 1].Item2;
                      tb[splitData.Count()+2].Background = Brushes.Green;
                      break;
                  }
      ~~~
  * 전체 가사 출력 후 현재 재생 가사 배경색 변경 버젼
    - 일반 곡, 파트 있는 곡, 일본어(3가사) 모두 완료
    - 3가사 버젼은 가사가 아닌 맨 앞 3줄과 맨 마지막 1줄 처리 필요
    
  * 전체 가사 출력 후 현재 재생 가사의 배경 색상 변경 버젼
     - 일반 가사 출력 완료
     - 파트 있는 가사 출력 완료
     - J-POP, POP SONG 등 한 시간 대의 여러 가사 출력 완료 ( 한 시간 대에 가사 2,3,4,5, ... 모두 가능)
    - 2/3 혼합 된 버젼도 출력 가능
    - 주석 완료
  * 예정 사항 : 버튼 클릭 시 전체 가사 버전 한줄 가사 버전 두가지 변경가능하도록 수정
  * 2개의 버튼을 통해 가사 출력 방식 다르게 설정
     - oneLien 버튼을 클릭하면 기존 방식대로 가사가 한줄 씩 출력 됨
     - allLine 버튼을 클릭하면 모든 가사가 출력 되고 현재 재생 가사의 배경 색만 변경
     - Default는 oneLine 출력 방식
     - 수정 필요한 사랑 
      1. UI 좀 더 이쁘게 디자인
       2. Dance The Night Away 까지 이상 없이 모두 출력 됨
      3. oneLine 출력 모드에서 JPOP/POP송 등 2줄 이상 가사 처리 필요
     -------------------------------------------------------------------------------------
   ### 2018-11-27
  * oneLine 출력 모드 / allLine 출력 모드 모두 완료
     - oneLine 모드에서 J-POP, POP SONG 등 2줄 이상의 가사 오류 해결
     - Windows 창 크기 조절
  * Slider를 통해 allLine 가사 출력 모드에서 fontSize 동적으로 조절
    - int allLineFontSize 변수 선언 
     - slider 이벤트가 발생할 때 마다 allLineFontSize 변수 값을 slider의 Value 값으 변경
    - allLineFontSize 가사 한줄 생성 시 마다 fontSize에 설정
   ----------------------------------------------------------------------------------------
   ### 2018-11-29
  * 전체 가사 출력 모드에서 재생하고자 하는 가사를 클릭 하면 재생위치를 클릭한 부분으로 변경하는 기능 추가
    - 각 텍스트 박스마다 PreviewMouseLeftButtonDown 이벤트 추가
    - 어떤 텍스트 박스에서 이벤트가 걸렸는지 알기 위해 텍스트 박스마다 인덱스를 포함해 Name 지정
    - 이벤트 함수에서 지정 된 이름을 통해 이벤트가 걸린 텍스트 박스를 불러옴
    - 이벤트가 걸린 텍스트 박스에 지정 된 재생시간으로  PrestoSDK.PrestoService.Player.Position(현재 재생 위치) 값 변경 
  * Info 버튼을 생성하여 버튼 클릭 시 현재 재생 중인 곡의 정보를 출력
    - 곡 정보로 가수, 곡명, 앨범을 출력
    - MessageBox.Show 를 통해 출력
    - 곡이 선택 되지 않았을 때는 에러 메세지 출력 (예외 처리)
  * 자료구조 변경
    - 한줄의 가사 TextBox로 되어 있던 변수들을 TextBlock으로 수정
    - TextBox는 Hover 시 쓸 때 없는 Border 가 생겨서 TextBlock 으로 수정

  * UI Design
    - allLine 가사 모드 시 제목 출력 Collapsed 시키고 oneLine 가사 출력 시 제목 출력 visible 시킴
    - Color Design
    - Font Design
    - Layout Design
    - Stream이 바뀔 때마다 가사 윈도우 창 맨 앞으로 가져오기
    -----------------------------------------------------------------------------------------------
    ### 2018-11-30
  * Material Design Package 를 활용한 고급적인 디자인 재구성
    - Package에 포함 된 아이콘을 활용하여 깔끔한 버튼 구성
    - 깔끔한 디자인을 위한 Layout 재구성
    - 가사 출력 화면 재구성

  * 기능 추가
    - 곡 변경 시 새 디자인에 맞게 곡 앨범 사진 변경
    - 일시 정지 기능 추가
    - 다음곡 재생, 이전 곡 재생 기능 추가
    - 종료 버튼 추가
