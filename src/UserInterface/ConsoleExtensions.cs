namespace Tesonet.ServerListApp.UserInterface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alba.CsConsoleFormat;
    using Application;

    public static class ConsoleExtensions
    {
        /// <summary>
        /// Prints a table of servers to console window
        /// using the provided collection of <see cref="Server"/>.
        /// </summary>
        /// <param name="servers"></param>
        public static void PrintToConsole(this IEnumerable<Server> servers)
        {
            List<(string Name, int Count)> groupedServers = servers
                .GroupBy(s => s.Name)
                .Select(g => (g.Key, g.Count()))
                .ToList();

            var grid = CreateGrid()
                .WithHeader()
                .WithBody(groupedServers)
                .WithFooter(groupedServers.Sum(s => s.Count));

            RenderDocument(new Document(grid));
        }

        private static void RenderDocument(Document document)
        {
            var target = (IRenderTarget)new ConsoleRenderTarget();
            var renderRect = new Rect?(ConsoleRenderer.DefaultRenderRect);
            var buffer = new ConsoleBuffer(renderRect.Value.Size.Width);
            ConsoleRenderer.RenderDocumentToBuffer(document, buffer, renderRect.Value);
            target.Render(buffer);
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

        private static Grid WithHeader(this Grid grid)
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

        private static Grid WithBody(this Grid grid, IEnumerable<(string Name, int Count)> groupedServers)
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

        private static Grid WithFooter(this Grid grid, int total)
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