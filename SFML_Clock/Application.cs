using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Diagnostics;

namespace SFML_Clock
{
	class Application
	{

		#region Global vars

		//The window of application
		private RenderWindow window;

		//Used to time the movement of the objects on screen
		Clock clock = new Clock();
		Time gameTime = new Time();

		#endregion

		float clockRadius;

		List<Drawable> drawObjects = new List<Drawable>();

		Text hours;

		CircleShape clockBack;

		/// <summary>
		/// Constructor of the window
		/// </summary>
		/// <param name="windowHeight">Height of the window</param>
		/// <param name="windowWidth">Width of the window</param>
		/// <param name="title">Title of the window</param>
		/// <param name="style">Style of the window</param>
		public Application(uint windowHeight = 300, uint windowWidth = 300, string title = "Clock", Styles style = Styles.Close)
		{

			ContextSettings settings = new ContextSettings();

			settings.AntialiasingLevel = 16;

			window = new RenderWindow(new VideoMode(windowWidth, windowHeight), title, style, settings);

			//Add the keypressed function to the window
			window.KeyPressed += window_KeyPressed;

			//Add the Closed function to the window
			window.Closed += window_Closed;

			clockRadius = Math.Min(windowHeight, windowWidth) / 2;

			//Create clock back
			clockBack = new CircleShape(clockRadius);

		}


		/// <summary>
		/// Main loop of the program
		/// </summary>
		public void Run()
		{

			drawObjects.Add(clockBack);

			for (int i = 1; i < 13; i++)
			{

				hours = new Text(""+i, new Font("arial.ttf"), 20);

				FloatRect hoursRect = hours.GetLocalBounds();

				//Center the text
				hours.Origin = new Vector2f(hoursRect.Left + hoursRect.Width / 2.0f, hoursRect.Top + hoursRect.Height / 2.0f);

				hours.Color = Color.Black;

				//This works after a couple of tests. clockRadius * .92 seems to be a good position with the scale of the font
				hours.Position = new Vector2f(	(float)Math.Cos((Math.PI * 1.5) + ((Math.PI / 6) * i)) * clockRadius * 0.92f + clockRadius, 
												(float)Math.Sin((Math.PI * 1.5) + ((Math.PI / 6) * i)) * clockRadius * 0.92f + clockRadius);

				drawObjects.Add(hours);
				
			}

			window.SetVisible(true);

			while (window.IsOpen)
			{

				//Call the Events
				window.DispatchEvents();

				//Update the game
				Update();

				//Draw the updated app
				Draw();

			}


		}

		#region Input functions

		/// <summary>
		/// Called whenever a key is pressed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e">The key event</param>
		void window_KeyPressed(object sender, KeyEventArgs e)
		{

			//Debug output of keyPressed
			//Console.WriteLine(e.Code);

			switch (e.Code)
			{

				case Keyboard.Key.Escape:
					window.Close();
					break;

				default:
					break;

			}
		}

		/// <summary>
		/// Called when the window "X" is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void window_Closed(object sender, EventArgs e)
		{

			window.Close();

		}

		#endregion

		/// <summary>
		/// Update code of the program
		/// </summary>
		private void Update()
		{

			gameTime = clock.Restart();

			

		}

		/// <summary>
		/// Draw code of the program
		/// </summary>
		private void Draw()
		{

			window.Clear();

			foreach (var item in drawObjects)
			{

				window.Draw(item);
				
			}

			window.Display();

		}

	}
}
