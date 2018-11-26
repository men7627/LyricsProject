/*
 * Desing Branch
 */
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
        TextBox[] tb;                            //가사 한줄 한줄의 배열
        List<Tuple<TimeSpan, string>> splitData; //파싱 데이터 
        public LyricsWindow()
        {
            InitializeComponent();
            PrestoSDK.PrestoService.Player.StreamChanged += Player_StreamChanged;  //스트림이 바뀌었을 때의 이벤트 함수
        }

        private void Player_StreamChanged(object sender, Common.StreamChangedEventArgs e)
        {

            //가사 초기화
            //textLyrics.Text = null;
            LyricPanel.Children.Clear(); //패널 초기화
            splitData = new List<Tuple<TimeSpan, string>>(); //파싱 데이터 리스트 초기화

            //lrc파일 경로 변경
            var fileName = PrestoSDK.PrestoService.Player.CurrentMusic.Path;
            var lrcName = Path.GetFileNameWithoutExtension(fileName) + ".lrc";
            var path = Path.Combine(Path.GetDirectoryName(fileName), lrcName);
            var lines = File.ReadAllLines(path);
            //가사 데이터 읽어 오기
            string singer = null;                    //파트 저장할 변수 
            tb = new TextBox[lines.Length];          //가사 한줄 한줄의 배열 생성
            for (int i = 3; i < lines.Length; i++)
            {
                tb[i] = new TextBox();               //가사 한줄 생성
                tb[i].BorderBrush = Brushes.White;   //가사 한줄의 테두리 흰색 설정

                string[] data = lines[i].Split(']'); //가사 한줄을 ']' 단위로 스플릿
                if (data.Length == 2)                //파트 지정이 없는 부분
                {
                    data[1] = singer + data[1] + '\n';      //이전 파트를 붙임
                    //textLyrics.Text += data[1];
                    tb[i].Text = data[1];                   //가사 한줄(텍스트 박스)에 가사 데이터 지정
                    LyricPanel.Children.Add(tb[i]);         //패널의 자식으로 가사 한줄 (텍스트 박스)를 지정
                }
                else if (data.Length == 3)           //파트 지정이 있는 부분
                {
                    singer = '(' + data[1].Substring(1).Trim() + ") "; 
                    data[1] = singer + data[2] + '\n';
                    tb[i].Text = data[1];
                    LyricPanel.Children.Add(tb[i]);
                    //textLyrics.Text += data[1];
                }
                TimeSpan time = TimeSpan.ParseExact(data[0].Substring(1).Trim(), @"mm\:ss\.ff", CultureInfo.InvariantCulture);
                string lyric = data[1];
                splitData.Add(new Tuple<TimeSpan, string>(time, lyric));
            }
            //노래 재생시간 타이머
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            for (int i = 3; i < tb.Length; i++)//모든 가사 배경 흰색
            {
                tb[i].Background = Brushes.White;  
            }
            //가사 변경 범위 조정
            for (int i = 0; i < splitData.Count(); i++)
            {
                var cur = TimeSpan.FromMilliseconds(PrestoSDK.PrestoService.Player.Position);
                if (cur < splitData[i].Item1) //첫 소절 나오기 전 간주 시간 공백 처리
                {
                    //textLyrics.Text = " ";
                    break;
                }
                else if (cur >= splitData[splitData.Count() - 1].Item1) //마지막 가사 출력 (인덱스 오류 방지)
                {
                    //textLyrics.Text = splitData[splitData.Count() - 1].Item2;
                    for (int j = 0; j <= splitData.Count() - 1; j++)
                    {
                        if (splitData[j].Item1 == splitData[splitData.Count() - 1].Item1) //마지막 가사의 시간 대의 모든 가사
                        {
                            tb[j+3].Background = Brushes.Green;
                        }
                    }
                    
                }
                else if (cur >= splitData[i].Item1 && cur < splitData[i + 1].Item1) //일반 가사 출력
                {
                    if (splitData[i].Item2 != null )
                    {
                        //textLyrics.Text = splitData[i].Item2;
                        for(int j = 0; j <= splitData.Count() - 1; j++) 
                        {
                            if (splitData[j].Item1 == splitData[i].Item1) //현재 시간 대의 모든 가사
                            {
                                tb[j + 3].Background = Brushes.Green;
                            }
                        }
                    }
                }
            }
        }
    }
}

