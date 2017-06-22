package com.example.demo;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

/**
 * Created by romain on 22/06/2017.
 */
@RestController
@RequestMapping(value = "/alarm", produces = "application/json", consumes = "application/json")
public class AlarmRestController {

    @Autowired private AlarmRepository repository;

    @RequestMapping(method = RequestMethod.POST)
    public AlarmDTO setAlarm(@RequestBody AlarmDTO alarm) {
        AlarmEntity entity = repository.findOne(alarm.id);
        if (entity != null) {
            entity.setHour(alarm.hour);
        }
        else {
            entity = new AlarmEntity();
            entity.setHour(alarm.hour);
            entity = repository.save(entity);
        }
        AlarmDTO res = entityToDto(entity);
        return res;
    }

    private AlarmDTO entityToDto(AlarmEntity entity) {
        AlarmDTO res = new AlarmDTO();
        res.id = entity.getId();
        res.hour = entity.getHour();
        return res;
    }

    @RequestMapping(method = RequestMethod.GET, value = "/{id}")
    public AlarmDTO getAlarmById(@PathVariable("id") int id) {
        AlarmEntity entity = repository.findOne(id);
        if (entity == null) {
            return null;
        }
        else {
            return entityToDto(entity);
        }
    }

}
