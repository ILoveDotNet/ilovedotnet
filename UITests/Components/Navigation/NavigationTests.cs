namespace UITests.Components.Navigation;

[ExcludeFromCodeCoverage]
public class NavigationTests
{
    [Fact]
    public void NavigationComponent_Renders_Correctly()
    {
        // Arrange
        using var ctx = new TestContext();

        // Act
        var cut = ctx.RenderComponent<Web.Shared.Navigation>();

        // Assert
        cut.MarkupMatches($@"<nav class=""[ fixed bottom-0 h-14 w-full md:top-14 md:left-0 md:h-full md:w-16 ] 
                                          [ bg-white ] [ dark:bg-gray-800 dark:bg-gray-800/80 ] [ backdrop-blur-sm bg-white/30 ] [ md:pt-2 ]
                                          [ flex justify-around items-center md:flex-col md:justify-start md:space-y-4 ]"">
                                  <a class=""[ flex flex-col items-center ] active"" href=""/"">
                                      <svg xmlns=""http://www.w3.org/2000/svg"" class=""[ icon icon-tabler icon-tabler-home ]"" width=""30"" height=""30"" viewBox=""0 0 24 24"" stroke-width=""1.5"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
                                          <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
                                          <polyline points=""5 12 3 12 12 3 21 12 19 12""></polyline>
                                          <path d=""M5 12v7a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-7""></path>
                                          <path d=""M9 21v-6a2 2 0 0 1 2 -2h2a2 2 0 0 1 2 2v6""></path>
                                      </svg>
                                      <span class=""[ text-xs text-center ]"">Home</span>
                                  </a>
                                  <a class=""[ flex flex-col items-center ]"" href=""/learningpath"">
                                          <svg xmlns=""http://www.w3.org/2000/svg"" class=""[ icon icon-tabler icon-tabler-compass ]"" width=""30"" height=""30"" viewBox=""0 0 24 24"" stroke-width=""1.5"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
                                          <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
                                          <polyline points=""8 16 10 10 16 8 14 14 8 16""></polyline>
                                          <circle cx=""12"" cy=""12"" r=""9""></circle>
                                          <line x1=""12"" y1=""3"" x2=""12"" y2=""5""></line>
                                          <line x1=""12"" y1=""19"" x2=""12"" y2=""21""></line>
                                          <line x1=""3"" y1=""12"" x2=""5"" y2=""12""></line>
                                          <line x1=""19"" y1=""12"" x2=""21"" y2=""12""></line>
                                      </svg>
                                      <span class=""[ text-xs text-center ]"">Learning Path</span>
                                  </a>
                                  <a class=""[ flex flex-col items-center ]"" href=""/disclaimer"">
                                      <svg xmlns=""http://www.w3.org/2000/svg"" class=""[ icon icon-tabler icon-tabler-notes ]"" width=""30"" height=""30"" viewBox=""0 0 24 24"" stroke-width=""1.5"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
                                          <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
                                          <rect x=""5"" y=""3"" width=""14"" height=""18"" rx=""2""></rect>
                                          <line x1=""9"" y1=""7"" x2=""15"" y2=""7""></line>
                                          <line x1=""9"" y1=""11"" x2=""15"" y2=""11""></line>
                                          <line x1=""9"" y1=""15"" x2=""13"" y2=""15""></line>
                                      </svg>
                                      <span class=""[ text-xs text-center ]"">Disclaimer</span>
                                  </a>
                                  <a class=""[ flex flex-col items-center ]"" href=""/privacy"">
                                      <svg xmlns=""http://www.w3.org/2000/svg"" class=""[ icon icon-tabler icon-tabler-lock ]"" width=""30"" height=""30"" viewBox=""0 0 24 24"" stroke-width=""1.5"" stroke=""currentColor"" fill=""none"" stroke-linecap=""round"" stroke-linejoin=""round"">
                                          <path stroke=""none"" d=""M0 0h24v24H0z"" fill=""none""></path>
                                          <rect x=""5"" y=""11"" width=""14"" height=""10"" rx=""2""></rect>
                                          <circle cx=""12"" cy=""16"" r=""1""></circle>
                                          <path d=""M8 11v-4a4 4 0 0 1 8 0v4""></path>
                                      </svg>
                                      <span class=""[ text-xs text-center ]"">Privacy</span>
                                  </a>
                              </nav>");
    }
}
