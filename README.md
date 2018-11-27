## 김효건 LyricsProject
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
