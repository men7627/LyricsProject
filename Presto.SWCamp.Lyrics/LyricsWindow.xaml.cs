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
        SortedList<TimeSpan, string> splitData; //파싱 데이터 
        public LyricsWindow()
        {
            InitializeComponent();
            //스트림이 바뀌었을 때의 이벤트 함수
            PrestoSDK.PrestoService.Player.StreamChanged += Player_StreamChanged;
        }

        private void Player_StreamChanged(object sender, Common.StreamChangedEventArgs e)
        {
            //가사 초기화
            textLyrics.Text = null;
            splitData = new SortedList<TimeSpan, string>();
            
            //lrc파일 경로 변경
            var fileName = PrestoSDK.PrestoService.Player.CurrentMusic.Path;
            var lrcName = Path.GetFileNameWithoutExtension(fileName) + ".lrc";
            var path = Path.Combine(Path.GetDirectoryName(fileName), lrcName);
            var lines = File.ReadAllLines(path);
            //가사 데이터 읽어 오기
            string singer = null;                    //파트 저장할 변수 
            for (int i = 3; i < lines.Length; i++)
            {
                string[] data = lines[i].Split(']'); 
                if (data.Length == 2)                //파트 지정이 없는 부분
                {
                    data[1] = singer + data[1];      //이전 파트를 붙임
                }
                else if (data.Length == 3)           //파트 지정이 있는 부분
                {
                    singer = '(' + data[1].Substring(1).Trim() + ") ";
                    data[1] = singer + data[2];
                }
                TimeSpan time = TimeSpan.ParseExact(data[0].Substring(1).Trim(), @"mm\:ss\.ff", CultureInfo.InvariantCulture);
                string lyric = data[1];
                splitData.Add(time, lyric);
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
            //가사 변경 범위 조정
            for (int i = 0; i < splitData.Count(); i++)
            {
                var cur = TimeSpan.FromMilliseconds(PrestoSDK.PrestoService.Player.Position);
                if(cur < splitData.Keys[0]) //첫 소절 나오기 전 간주 시간 공백 처리
                {
                    textLyrics.Text = " ";
                    break;
                }
                else if (cur >= splitData.Keys[splitData.Count() - 1]) //마지막 가사 출력 (인덱스 오류 방지)
                {
                    textLyrics.Text = splitData.Values[splitData.Count() - 1];
                    break;
                }
                else if (cur >= splitData.Keys[i] && cur < splitData.Keys[i + 1]) //일반 가사 출력
                {
                    if (splitData.Values[i] != null)
                    {
                        textLyrics.Text = splitData.Values[i];  
                        break;
                    }
                }
                
            }
        }
    }
}
