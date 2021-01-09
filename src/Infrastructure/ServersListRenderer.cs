namespace Tesonet.ServerListApp.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alba.CsConsoleFormat;
    using Application;

    public class ServersListRenderer
    {
        private readonly bool _renderToCommandLine;

        public ServersListRenderer(bool renderToCommandLine = true)
        {
            _renderToCommandLine = renderToCommandLine;
        }

        /// <summary>
        /// Prints a table of servers to console window
        /// using the provided collection of <see cref="Server"/>.
        /// </summary>
        /// <param name="servers"></param>
        public void Render(IEnumerable<Server> servers)
        {
            List<(string Name, int Count)> groupedServers = servers
                .GroupBy(s => s.Name)
                .Select(g => (g.Key, g.Count()))
                .ToList();

            var grid = CreateGrid();
            grid = WithHeader(grid);
            grid = WithBody(grid, groupedServers);
            grid = WithFooter(grid, groupedServers.Sum(s => s.Count));
            var document = new Document(grid);

            if (!_renderToCommandLine)
            {
                return;
            }

            var renderRect = new Rect?(ConsoleRenderer.DefaultRenderRect);
            var buffer = new ConsoleBuffer(renderRect.Value.Size.Width);
            ConsoleRenderer.RenderDocumentToBuffer(document, buffer, renderRect.Value);

            IRenderTarget target = new ConsoleRenderTarget();
            target.Render(buffer);

            /*
             * How to render to string:
             * TextRenderTargetBase target = new TextRenderTarget();
             * target.Render(buffer);
             * Output = target.OutputText;
             */
        }

        private static Grid CreateGrid()
        {
            return new()
            {
                Stroke = LineThickness.Single,
                StrokeColor = ConsoleColor.Blue,
                Columns =
                {
                    new Column { Width = GridLength.Star(1) },
                    new Column { Width = GridLength.Char(20) }
                }
            };
        }

        private static Grid WithHeader(Grid grid)
        {
            grid.Children.Add(new Cell
            {
                Stroke = LineThickness.None,
                Background = ConsoleColor.Gray,
                TextAlign = TextAlign.Center,
                Color = ConsoleColor.Black,
                Children = { "Name" }
            });

            grid.Children.Add(new Cell
            {
                Stroke = LineThickness.None,
                TextAlign = TextAlign.Center,
                Background = ConsoleColor.Gray,
                Color = ConsoleColor.Black,
                Children = { "Count" }
            });

            return grid;
        }

        private static Grid WithBody(Grid grid, IEnumerable<(string Name, int Count)> groupedServers)
        {
            var random = new Random();

            foreach (var (name, count) in groupedServers)
            {
                // Since ConsoleColor has 16 values defined, let's get a random one (except black)
                var textColor = (ConsoleColor)random.Next(1, 16);

                grid.Children.Add(new Cell
                {
                    Stroke = LineThickness.Single,
                    Color = textColor,
                    Children = { name }
                });

                grid.Children.Add(new Cell
                {
                    Stroke = LineThickness.Single,
                    Color = textColor,
                    Children = { count }
                });
            }

            return grid;
        }

        private static Grid WithFooter(Grid grid, int total)
        {
            grid.Children.Add(new Cell
            {
                Stroke = LineThickness.Single,
                Color = ConsoleColor.White,
                TextAlign = TextAlign.Right,
                Children = { "Total:" }
            });

            grid.Children.Add(new Cell
            {
                Stroke = LineThickness.Single,
                Color = ConsoleColor.White,
                Children = { total }
            });

            return grid;
        }
    }
}