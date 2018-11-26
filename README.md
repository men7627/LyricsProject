## 김효건 LyricsProject
#### 2018-11-24
 * 싱크에 맞춰서 가사 출력하기
 * 배열 형태로 가사를 저장한 것을 SortedList로 변경
   - (메모리 낭비 줄이기 위해 가변적인 자료구조 사용)
 * 마지막 가사 출력 후 인덱스 초과 오류 방지 수정
 * Twice Dance The Night Away 형식 실행 가능하도록 수정
   - (plit 했을 때 3개인 경우와 2개인 경우로 나누어서 이전의 파트 없는 노래도 문제없이 재생 가능)
 * 파싱한 데이터를 저장하는 변수를 SortedList<TimeSpan,string>에서 List<Tuple<TimeSpan,string>>으로 변경
   - (일본곡 재생을 위해서 동일한 키 값 사용을 위해)
-------------------------------------------------------------------------------------
 #### 2018-11-26
  * 브랜치로 빼서 가사 출력을 전체가사를 출력하고 재생되는 가사의 배경만 색을 바꾸는 식으로 수정
    - 스크롤 추가 
    - 마지막 가사 오류
  * 마지막 가사 오류 수정
    - (인덱스 수정)
    - <code>
      else if (cur >= splitData[splitData.Count() - 1].Item1) //마지막 가사 출력 (인덱스 오류 방지)
                  {
                      //textLyrics.Text = splitData[splitData.Count() - 1].Item2;
                      tb[splitData.Count()+2].Background = Brushes.Green;
                      break;
                  }
      </code>
