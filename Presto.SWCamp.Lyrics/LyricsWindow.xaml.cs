using Presto.SDK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
namespace Presto.SWCamp.Lyrics
{
    public partial class LyricsWindow : Window
    {
        bool pauseIsClicked = false;              //일시정지 버튼 
        bool starting = false;                    //프로그램 시작
        string songInfo = " ";                    //곡 정보를 저장할 변순        
        TimeSpan cur;                             //현재 시간 저장할 변수
        int allLineFontSize = 12;                 //전체 가사 font size
        bool oneLineIsClicked = false;            //1줄 가사 버튼 클릭 
        bool allLineIsClicked = false;            //전체 가사 버튼 클릭
        TextBlock[] tb;                           //가사 한줄 한줄의 배열
        TextBlock one;                            //한줄 가사 모드에서의 현재 재생가사 변수
        List<Tuple<TimeSpan, string>> splitData;  //파싱 데이터 

        public LyricsWindow()
        {
            InitializeComponent();
            PrestoSDK.PrestoService.Player.StreamChanged += Player_StreamChanged;  //스트림이 바뀌었을 때의 이벤트 함수
        }

        private void Player_StreamChanged(object sender, Common.StreamChangedEventArgs e)
        {
            starting = true; //첫 재생
            Topmost = true; // 스트림이 바뀔 때 마다 가사 창 맨 앞으로 가져오기
            Topmost = false;

            //가사 초기화
            textLyrics.Text = null;
            LyricPanel.Children.Clear(); //패널 초기화
            splitData = new List<Tuple<TimeSpan, string>>(); //파싱 데이터 리스트 초기화
            songInfo = " ";                               //곡 정보 초기화

            //lrc파일 경로 변경
            var fileName = PrestoSDK.PrestoService.Player.CurrentMusic.Path;
            var lrcName = Path.GetFileNameWithoutExtension(fileName) + ".lrc";
            //Image 파일 변경
            String imgPath = "C:\\Users\\Admin\\LyricsProject\\Presto.SWCamp.Lyrics\\" + "Assets\\" + Path.GetFileNameWithoutExtension(fileName) + ".jpg";
            Uri imageUri = new Uri(imgPath,UriKind.Absolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            Image myImage = new Image();
            myImage.Source = imageBitmap;
            songImg.ImageSource = myImage.Source;
            var path = Path.Combine(Path.GetDirectoryName(fileName), lrcName);

             

            //가사 데이터 읽어 오기
            var lines = File.ReadAllLines(path);
            string singer = null;                    //파트 저장할 변수 
            tb = new TextBlock[lines.Length];          //가사 한줄 한줄의 배열 생성

            //곡 정보 저장
            songInfo += "가수 : " + (lines[0].Substring(4)).Split(']').First() + '\n';
            songInfo += "제목 : " + (lines[1].Substring(4)).Split(']').First() + '\n';
            songInfo += "앨범 : " + (lines[2].Substring(4)).Split(']').First() + '\n';

            for (int i = 3; i < lines.Length; i++)
            {
                tb[i] = new TextBlock();               //가사 한줄 생성
                
                string[] data = lines[i].Split(']'); //가사 한줄을 ']' 단위로 스플릿

                if (data.Length == 2)                //파트 지정이 없는 부분
                {
                    data[1] = singer + data[1] + '\n';      //이전 파트를 붙임
                    tb[i].Text = data[1];                   //가사 한줄(텍스트 박스)에 가사 데이터 지정
                }

                else if (data.Length == 3)           //파트 지정이 있는 부분
                {
                    singer = '(' + data[1].Substring(1).Trim() + ") ";
                    data[1] = singer + data[2] + '\n';
                    tb[i].Text = data[1];
                }

                TimeSpan time = TimeSpan.ParseExact(data[0].Substring(1).Trim(), @"mm\:ss\.ff", CultureInfo.InvariantCulture); //시간 데이터
                string lyric = data[1]; //가사 데이터
                splitData.Add(new Tuple<TimeSpan, string>(time, lyric));      //가사 한줄 데이터 추가            
            }

            //노래 재생시간 타이머
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };

            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) //Timer 마다 실행 될 함수
        {
            if (oneLineIsClicked == false && allLineIsClicked == false) // Default
                oneLineAction();

            else if (oneLineIsClicked == true) //oneLine 출력 모드
            {
                oneLineAction();
            }

            else if (allLineIsClicked == true) //allLine 출력 모드
            {
                allLineAction();
            }
        }

        private void oneLineAction()
        {
            LyricPanel.Children.Clear(); //패널 초기화
            for (int i = 0; i < splitData.Count(); i++)
            {
                cur = TimeSpan.FromMilliseconds(PrestoSDK.PrestoService.Player.Position); //현재 재생 되는 위치

                if (cur < splitData[i].Item1) //첫 소절 나오기 전 간주 시간 공백 처리
                {
                    one = new TextBlock();
                    one.Text = " ";
                    one.Background = Brushes.Black;
                    LyricPanel.Children.Add(one);         //패널의 자식으로 가사 한줄 (텍스트 박스)를 지정
                    break;
                }

                else if (cur >= splitData[splitData.Count() - 1].Item1) //마지막 가사 출력 (인덱스 오류 방지)
                {
                    for (int j = 0; j <= splitData.Count() - 1; j++)
                    {
                        if (splitData[j].Item1 == splitData[splitData.Count() - 1].Item1) //마지막 가사의 시간 대의 모든 가사
                        {
                            one = new TextBlock();                      //텍스트 박스 생성
                            one.FontSize = 18;
                            one.Text = splitData[j].Item2;
                            LyricPanel.Children.Add(one);         //패널의 자식으로 가사 한줄 (텍스트 박스)를 지정
                        }
                    }
                    break;
                }

                else if (cur >= splitData[i].Item1 && cur < splitData[i + 1].Item1) //일반 가사 출력
                {
                    for (int j = 0; j <= splitData.Count() - 1; j++)
                    {
                        if (splitData[j].Item1 == splitData[i].Item1) //현재 가사의 시간 대의 모든 가사
                        {
                            one = new TextBlock();                      //텍스트 박스 생성
                            one.Background = Brushes.Black;
                            one.FontSize = 18;
                            one.Text = splitData[j].Item2;
                            LyricPanel.Children.Add(one);         //패널의 자식으로 가사 한줄 (텍스트 박스)를 지정
                        }
                    }
                    break;
                }
            }
        }

        private void allLineAction()
        {
            LyricPanel.Children.Clear(); //패널 초기화
            for (int i = 3; i < tb.Length; i++) //전체 가사 생성
            {
                tb[i].Name = 't' + i.ToString();        //인덱스에 따라 이름 지정 -> 몇 번째 텍스트 박스가 클릭 됐는지 알기 위해
                tb[i].Foreground = Brushes.LightSteelBlue;       //모든 가사 배경 흰색
                tb[i].FontSize = allLineFontSize;       //slider의 값에 따라 fontSize 변경
                tb[i].Cursor = Cursors.Hand;            //커서 효과 변경
                LyricPanel.Children.Add(tb[i]);         //패널의 자식으로 가사 한줄 (텍스트 박스)를 지정
                tb[i].PreviewMouseLeftButtonDown += click_lyric;
            }
            //가사 변경 범위 조정
            for (int i = 0; i < splitData.Count(); i++)
            {
                cur = TimeSpan.FromMilliseconds(PrestoSDK.PrestoService.Player.Position);

                if (cur >= splitData[splitData.Count() - 1].Item1) //마지막 가사 출력 (인덱스 오류 방지)
                {
                    for (int j = 0; j <= splitData.Count() - 1; j++)
                    {
                        if (splitData[j].Item1 == splitData[splitData.Count() - 1].Item1) //마지막 가사의 시간 대의 모든 가사
                        {
                            tb[j + 3].Foreground = Brushes.LimeGreen; //배경 색 변경
                        }
                    }
                }

                else if (cur >= splitData[i].Item1 && cur < splitData[i + 1].Item1) //일반 가사 출력
                {
                    if (splitData[i].Item2 != null)
                    {
                        for (int j = 0; j <= splitData.Count() - 1; j++)
                        {
                            if (splitData[j].Item1 == splitData[i].Item1) //현재 시간 대의 모든 가사
                            {
                                tb[j + 3].Foreground = Brushes.LimeGreen; //배경 색 변경
                            }
                        }
                    }
                }
            }
        }

        private void oneLine_Click(object sender, RoutedEventArgs e) //한줄 출력 형식 버튼 클릭 시
        {
            if (allLineIsClicked == true)
                allLineIsClicked = false;
            oneLineIsClicked = true;
            textTitle.Visibility = Visibility.Visible; //제목 가시화
        }

        private void allLine_Click(object sender, RoutedEventArgs e) //전체 출력 형식 버튼 클릭 시
        {
            textTitle.Visibility = Visibility.Collapsed; //제목 없앰
            if (oneLineIsClicked == true)
                oneLineIsClicked = false;
            allLineIsClicked = true;
        }

        private void click_lyric(object sender, RoutedEventArgs e) //텍스트 박스 클릭 이벤트
        {
            TextBlock m = e.Source as TextBlock;                       //이벤트가 걸린 텍스트 박스를 가져옴
            int index = int.Parse(m.Name.Substring(1).ToString()); //텍스트 박스 Name을 통해 몇번째인지 인덱스 지정
            PrestoSDK.PrestoService.Player.Position = double.Parse(splitData[index - 3].Item1.TotalMilliseconds.ToString()); //클릭 된 텍스트 박스에 지정 된 시간으로 현재 재생 위치 변경

            for (int i = 0; i < splitData.Count(); i++)
            {
                if (cur >= splitData[splitData.Count() - 1].Item1) //마지막 가사 출력 (인덱스 오류 방지)
                {
                    for (int j = 0; j <= splitData.Count() - 1; j++)
                    {
                        if (splitData[j].Item1 == splitData[splitData.Count() - 1].Item1) //마지막 가사의 시간 대의 모든 가사
                        {
                            tb[j + 3].Foreground = Brushes.LimeGreen; //배경 색 변경
                        }
                    }
                }

                else if (cur >= splitData[i].Item1 && cur < splitData[i + 1].Item1) //일반 가사 출력
                {
                    if (splitData[i].Item2 != null)
                    {
                        for (int j = 0; j <= splitData.Count() - 1; j++)
                        {
                            if (splitData[j].Item1 == splitData[i].Item1) //현재 시간 대의 모든 가사
                            {
                                tb[j + 3].Foreground = Brushes.LimeGreen; //배경 색 변경
                            }
                        }
                    }
                }
            }
        }

        private void info_Click(object sender, RoutedEventArgs e) //곡 정보를 출력하는 이벤트 함수
        {
            if (starting == false && allLineIsClicked == false && oneLineIsClicked == false) // 곡이 재생 중이지 않을 때 예외처리
                MessageBox.Show("재생 중인 곡이 없습니다.");
            else
                MessageBox.Show(songInfo);
        }

        private void exitButton(object sender, RoutedEventArgs e)        // 종료 버튼 누를 시 프로그램 종료
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)    //일시정지 버튼
        {
            if(pauseIsClicked == false)
            {
                PrestoSDK.PrestoService.Player.Pause();                //재생 중일 때는 정지
                pauseIsClicked = true;
            }
            else
            {
                PrestoSDK.PrestoService.Player.Resume();               //정지 중일 때는 재생
                pauseIsClicked = false;
            }
        }

        private void backClick(object sender, RoutedEventArgs e)    //이전 곡 재생
        {
            PrestoSDK.PrestoService.Player.PlayPrevious();
        }

        private void frontClick(object sender, RoutedEventArgs e)   //다음 곡 재생
        {
            PrestoSDK.PrestoService.Player.PlayNext();
        }

        private void clickPlus(object sender, RoutedEventArgs e)    //전체 가사 모드에서의 글자 확대
        {
            if (allLineFontSize == 24)
                allLineFontSize = 24;
            else
                allLineFontSize++;
                    
        }

        private void clickMinus(object sender, RoutedEventArgs e)   //전체 가사 모드에서의 글자 축소
        {
            if (allLineFontSize == 10)
                allLineFontSize = 10;
            else
                allLineFontSize--;
        }
    }
}