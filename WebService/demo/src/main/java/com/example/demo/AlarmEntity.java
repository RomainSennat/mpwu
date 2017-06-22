package com.example.demo;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

/**
 * Created by romain on 22/06/2017.
 */
@Entity
public class AlarmEntity {

    @Id
    @GeneratedValue
    private int id;

    private String hour;

    public String getHour() {
        return hour;
    }

    public void setHour(String hour) {
        this.hour = hour;
    }

    public AlarmEntity() {}

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
