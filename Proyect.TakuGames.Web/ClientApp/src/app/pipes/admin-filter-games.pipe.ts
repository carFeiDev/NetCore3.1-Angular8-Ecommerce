import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'adminFilterGames'
})
export class AdminFilterGamesPipe implements PipeTransform {

  transform(games: any, text: any): any {
    if (text == undefined || text.length < 3) return games;
    return games.filter(game => {
      return game.title.toLowerCase(text.toLowerCase())
        .includes(text.toLowerCase());
    })
  }

}
