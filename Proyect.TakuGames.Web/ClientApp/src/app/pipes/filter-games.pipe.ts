import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterGames'
})
export class FilterGamesPipe implements PipeTransform {

  transform(games: any, text: any): any {
    if (text == undefined || text.length < 3) return games;
    return games.filter(game => {
      return game.title.toLowerCase(text.toLowerCase())
        .includes(text.toLowerCase());
    })
  }
    //  if (text == undefined || text.length < 3) return games;
    //  let resultgames = [];
    //  for (let game of games) {
    //    if (game.name.toLowerCase().indexOf(text.toLowerCase()) > -1) {
    //      resultgames.push(game);
    //    };
    //  };
    //  return resultgames;
    //}
}
